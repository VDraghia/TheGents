using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models;
using ProjectManagementCollection.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Controllers
{
    public class DocumentController : Controller
    {

        private readonly ILogger<DocumentController> _logger;
        private readonly PmcAppDbContext _context;

        public DocumentController(PmcAppDbContext context, ILogger<DocumentController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("~/Document/SearchDocuments")]
        [Route("~/Document/SearchDocuments/{id}")]
        public IActionResult SearchDocuments()
        {
            return View();
        }

        [HttpPost]
        [Route("~/Document/SearchDocuments")]
        [Route("~/Document/SearchDocuments/{id}")]
        public async Task<IActionResult> SearchDocuments(SearchDocumentModel modelFromView)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            IList<Document> docs = _context.Documents.Where(p => p.Name.Contains(modelFromView.DocumentName)).ToList();

            // Model for view
            SearchDocumentModel docModel = new SearchDocumentModel();

            string queryDocuments = @"SELECT *
                                    FROM dbo.Documents doc
                                    INNER JOIN dbo.DocumentFactorRel rel ON
                                    doc.DocumentId = rel.DocumentId 
                                    WHERE 1=1";

            // Add Must Have Factors if any
            if (modelFromView.MustHaveFactors.Count() > 0)
            {
                // Append " AND " before first Factor
                queryDocuments += " AND ";

                // Loop to add Factors
                for (int i = 0; i < modelFromView.MustHaveFactors.Count(); i++)
                {

                    queryDocuments += "rel.FactorId = " + modelFromView.MustHaveFactors.ElementAt(i).FactorId;

                    // Append 'AND' between Factors
                    if (i + 1 < modelFromView.MustHaveFactors.Count() && modelFromView.MustHaveFactors.Count() != 1)
                    {
                        queryDocuments += " AND ";
                    }
                }
            }

            // Add Must Have Factors if any
            if (modelFromView.NotHaveFactors.Count() > 0)
            {

                // Append " AND NOT" before first Factor
                queryDocuments += " AND NOT (";

                // Loop to add Factors
                for (int i = 0; i < modelFromView.NotHaveFactors.Count(); i++)
                {
                    queryDocuments += "rel.FactorId = " + modelFromView.NotHaveFactors.ElementAt(i).FactorId;

                    // Append 'OR' between Factors but not if last Factor
                    if (i + 1 < modelFromView.NotHaveFactors.Count() && modelFromView.NotHaveFactors.Count() != 1)
                    {
                        queryDocuments += " OR ";
                    }
                }

                queryDocuments += ")";
            }

            // Run query
            docModel.Documents = await _context.Documents.FromSqlRaw(queryDocuments).ToListAsync();

            // Get All Project
            IList<Project> projects = await _context.Projects.ToListAsync();

            // Create Project id:name relation
            foreach (var proj in projects)
            {
                docModel.ProjectNames.Add(proj.ProjectId, proj.Name);
            }

            return View(docModel);
        }

        [HttpGet]
        [Route("~/Document/Upload")]
        [Route("~/Document/Upload/{id}")]
        public IActionResult Upload([FromRoute] int? id)
        {

            UploadDocumentModel uploadModel = new UploadDocumentModel();
            Project proj = new Project();

            // Return Project Name
            if (id != null)
            {
                proj = _context.Projects.Where(p => p.ProjectId == id).Single();
            }

            // Populate text field with empty string
            if (proj.ProjectId == 0)
            {
                uploadModel.ProjectName = "";
            }

            uploadModel.ProjectName = proj.Name;

            //Get all factors and categories for description
            IList<Factor> factors = _context.Factors.ToList();
            IList<FactorMainCategory> mainCategories = _context.FactorMainCategories.ToList();
            IList<FactorSubCategory> subCategories = _context.FactorSubCategories.ToList();

            IList<ListFactorDescriptor> listFactors = new List<ListFactorDescriptor>();

            //Build Factor descriptor list to display
            foreach (var fac in factors)
            {
                //Get Category description
                FactorMainCategory mainDesc = mainCategories.Where(c => c.FactorMainCategoryId == fac.FactorMainCategoryFk).Single();
                FactorSubCategory subDesc = subCategories.Where(c => c.FactorSubCategoryId == fac.FactorSubCategoryFk).Single();

                //Build new List factor descriptor
                ListFactorDescriptor newListModel = new ListFactorDescriptor()
                {
                    FactorId = fac.FactorId,
                    Position = fac.Position,
                    MainCategoryDesc = mainDesc.FactorMainCategoryDesc,
                    SubCategoryDesc = subDesc.FactorSubCategoryDesc
                };

                listFactors.Add(newListModel);
            }

            /*
             * Order factors by position number to place them in sequential category
             * This simplifies displaying on page.
             */
            uploadModel.ListFactorDesc = listFactors.OrderBy(f => f.Position).ToList();

            return View(uploadModel);
        }


        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        public async Task<IActionResult> Upload(UploadDocumentModel modelFromView)
        {

            UploadDocumentModel modelToView = new UploadDocumentModel();

            //
            if (modelFromView.ProjectName == null)
            {
                modelToView.Error = true;
                modelToView.Message = "Please enter an existing project name.";
                return RedirectToAction("Upload", "Document");
            }

            /*
             * AWS Credentials
             */
            string AWS_accessKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_accessKey"];
            string AWS_secretKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_secretKey"];
            string AWS_bucketName = "gentsproject2";

            // Check project exists
            Project existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.Name == modelFromView.ProjectName);

            // Return to upload page if project does not exit
            if (existingProject != null)
            {
                modelToView.Error = true;
                modelToView.Message = "Project does not exist. Create a project first.";
                return RedirectToAction("Upload", "Document");
            }

            try
            {
                AmazonS3Client s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.CACentral1);

                string newKeyName = existingProject + "/" + modelFromView.File.FileName;
                var fs = modelFromView.File.OpenReadStream();

                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = AWS_bucketName,
                    Key = newKeyName,
                    InputStream = fs,
                    ContentType = modelFromView.File.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                await s3Client.PutObjectAsync(request);


                string result = string.Format("http://{0}.s3.amazonaws.com/{1}", AWS_bucketName, newKeyName);

                //Create new document
                Document doc = new Document()
                {
                    Name = modelFromView.File.FileName,
                    Url = newKeyName,
                    ProjectFk = existingProject.ProjectId
                };

                //Save document
                _context.Documents.Add(doc);
                await _context.SaveChangesAsync();

                //Create document factor relation and add to database
                foreach (var factor in modelFromView.ListFactorDesc)
                {
                    if (factor.Checked)
                    {
                        DocumentFactorRel docFacRel = new DocumentFactorRel();
                        docFacRel.FactorFk = factor.FactorId;
                        docFacRel.DocumentFk = doc.DocumentId;

                        _context.DocumentFactorRels.Add(docFacRel);
                    }
                }

                await _context.SaveChangesAsync();
                modelToView.Message = "Uploaded Successfully!!";
            }
            catch (Exception ex)
            {
                modelToView.Error = true;
                modelToView.Message = ex.Message;
            }

            // Empty the project name string before returning to upload page
            modelToView.ProjectName = "";

            return View(modelToView);
        }

        public IActionResult ViewDocument(int id)
        {

            ViewDocumentModel model = new ViewDocumentModel();

            // Get Document and Project
            try { 
            model.Document = _context.Documents.Where(c => c.DocumentId == id).Single();
            model.Project = _context.Projects.Where(p => p.ProjectId == model.Document.ProjectFk).Single();
            } catch (Exception ex)
            {
                _logger.LogError("Could not find Document or Project");
                return View();
            }
            //Get the project factor relationships
            List<DocumentFactorRel> docFactorsRels = _context.DocumentFactorRels.Where(c => c.DocumentFk == id).ToList();

            IList<Factor> factors = new List<Factor>();

            // Get the Factors related to the Projects
            foreach (DocumentFactorRel docFacRel in docFactorsRels)
            {
                factors.Add(_context.Factors.Single(c => c.FactorId == docFacRel.FactorFk));
            }

            //Get all factors and categories for description
            IList<FactorMainCategory> mainCategories = _context.FactorMainCategories.ToList();
            IList<FactorSubCategory> subCategories = _context.FactorSubCategories.ToList();

            IList<ListFactorDescriptor> listFactors = new List<ListFactorDescriptor>();

            //Build Factor descriptor list to display
            foreach (var fac in factors)
            {
                //Get Category description
                FactorMainCategory mainDesc = mainCategories.Where(c => c.FactorMainCategoryId == fac.FactorMainCategoryFk).Single();
                FactorSubCategory subDesc = subCategories.Where(c => c.FactorSubCategoryId == fac.FactorSubCategoryFk).Single();

                //Build new List factor descriptor
                ListFactorDescriptor factorDescriptor = new ListFactorDescriptor()
                {
                    FactorId = fac.FactorId,
                    Position = fac.Position,
                    MainCategoryDesc = mainDesc.FactorMainCategoryDesc,
                    SubCategoryDesc = subDesc.FactorSubCategoryDesc
                };

                listFactors.Add(factorDescriptor);
            }

            listFactors.OrderBy(f => f.Position);

            model.Factors = listFactors;

            return View(model);
        }
    }
}
