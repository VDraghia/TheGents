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
using ProjectManagementCollection.Models.DescriptorModels;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ProjectManagementCollection.Controllers
{
    public class DocumentController : Controller
    {

        private readonly ILogger<DocumentController> _logger;
        private readonly PmcAppDbContext _context;

        /*
        * AWS Credentials
        */
        string AWS_accessKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_accessKey"];
        string AWS_secretKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_secretKey"];
        string AWS_bucketName = "gentsproject";

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
            if(HomeController.current_role == 0)
            {
                return RedirectToAction("Login", "Home");
            }

            SearchDocumentModel viewModel = new SearchDocumentModel();
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
            viewModel.ListFactorDesc = listFactors.OrderBy(f => f.Position).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [Route("~/Document/SearchDocuments")]
        [Route("~/Document/SearchDocuments/{id}")]
        public IActionResult SearchDocuments(SearchDocumentModel modelFromView)
        {
            if (HomeController.current_role == 0)
            {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            IList<Document> docs;

            if (modelFromView.DocumentName != null)
            {
                docs = _context.Documents.Where(p => p.Name.Contains(modelFromView.DocumentName)).ToList();
            }
            else
            {
                docs = _context.Documents.ToList();
            }

            // Model for view
            SearchDocumentModel docModel = new SearchDocumentModel();
            docModel.Documents = new List<Document>();

            // Add Must Have Factors if any
            if (modelFromView.MustHaveFactors != null && modelFromView.MustHaveFactors.Count() > 0)
            {
                for (int i = 0; i < modelFromView.MustHaveFactors.Count(); i++)
                {
                    List<Document> temp = new List<Document>();
                    foreach (var doc in docs)
                    {
                        if (_context.DocumentFactorRels.Where(dr => dr.DocumentFk == doc.DocumentId).Where(dr => dr.FactorFk == modelFromView.MustHaveFactors.ElementAt(i)).Count() == 1)
                        {
                            docModel.Documents.Add(doc);
                        }
                    }
                }
            } else
            {
                docModel.Documents = docs;
            }


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
            docModel.ListFactorDesc = listFactors.OrderBy(f => f.Position).ToList();



            return View(docModel);
        }


        [HttpGet]
        [Route("~/Document/Upload")]
        [Route("~/Document/Upload/{id}")]
        public async Task<IActionResult> Upload([FromRoute] int? id)
        {
            if (HomeController.current_role == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            UploadDocumentModel uploadModel = new UploadDocumentModel();

            Project proj = new Project();

            // Return Project Name
            if (id != null)
            {
                // clear the model ProjectId
                //uploadModel.ProjectId = (int)id;
                proj = await _context.Projects.FirstOrDefaultAsync(u => u.ProjectId == id);
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
        public async Task<IActionResult> UploadConfirm(IFormFile uploadFile, UploadDocumentModel modelFromView)
        {
            if (HomeController.current_role == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            UploadDocumentModel modelToView= new UploadDocumentModel();

            //
            if (modelFromView.ProjectName == null)
            {
                //modelToView.Error = true;
                //modelToView.Message = "Please enter an existing project name.";
                TempData["message"] = "Please enter an existing project name.";
                return RedirectToAction("Upload", "Document");
            }

            // Check project exists
            Project existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.Name == modelFromView.ProjectName);

            // Return to upload page if project does not exit
            if (existingProject == null)
            {
                //modelToView.Error = true;
                //modelToView.Message = "Project does not exist. Create a project first.";
                TempData["message"] = "Please enter an existing project name.";
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
                // modelToView.Message = "Uploaded Successfully!!";
                TempData["message"] = "Uploaded Successfully!!";
            }
            catch (Exception ex)
            {
                modelToView.Error = true;
                modelToView.Message = ex.Message;
            }

            return RedirectToAction("Upload", "Document");
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
                _logger.LogError("Could not find Document or Project", ex);
                return View();
            }
            //Get the project factor relationships
            List<DocumentFactorRel> docFactorsRels = _context.DocumentFactorRels.Where(c => c.DocumentFk == id).ToList();


            List<Factor> factors = new List<Factor>();

            //Get the document factor relationships
            List<DocumentFactorRel> docFactors = _context.DocumentFactorRels.Where(c => c.DocumentFk == id).ToList();
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


        [HttpPost]
        public async Task<IActionResult> Download(string url)
        {
            var s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.CACentral1);

            string[] keySplit = url.Split('/');
            string fileName = keySplit[keySplit.Length - 1];


            GetObjectRequest request1 = new GetObjectRequest();
            request1.BucketName = AWS_bucketName + "/" + keySplit[0];
            request1.Key = fileName;

            using GetObjectResponse response = await s3Client.GetObjectAsync(request1);
            using Stream responseStream = response.ResponseStream;
            var stream = new MemoryStream();
            await responseStream.CopyToAsync(stream);
            stream.Position = 0;

            string contentType = response.Headers["Content-Type"];
            var token = new CancellationToken();
            await response.WriteResponseStreamToFileAsync(null, false, token);

            TempData["message"] = fileName;

            return RedirectToAction("ViewDocument");
        }


        [HttpPost]
        public IActionResult FavoriteDoc()
        {
            var url = Request.Form["urldoc"].ToString();
            var command = Request.Form["cmdDoc"].ToString();
            if (command.Equals("Display") && !url.Equals(""))
            {
                string[] urlSplit = url.Split('/');
                string favorUrl = urlSplit[urlSplit.Length - 2] + "/" + urlSplit[urlSplit.Length - 1].Replace("+", " ");
                var docId = _context.Documents.Where(d => d.Url.Equals(favorUrl)).FirstOrDefault().DocumentId;
                TempData["link"] = url;
                return RedirectToAction("ViewDocument", "Document", new { id = docId });
            }
            else if (command.Equals("Remove") && !url.Equals(""))
            {
                // favorDoc url stroed format is: projectName/documentName.ext(should change seeding data format to match this)
                string[] urlSplit = url.Split('/');
                string favorUrl = urlSplit[urlSplit.Length - 2] + "/" + urlSplit[urlSplit.Length - 1].Replace("+", " ");
                var docId = _context.Documents.Where(d => d.Url.Equals(favorUrl)).FirstOrDefault().DocumentId;
                FavorDoc doc = _context.FavorDocs.Where(p => p.DocumentId == docId).Single();
                _context.FavorDocs.Remove(doc);
                _context.SaveChanges();
                TempData["message"] = "Removed the document successfully!!";
                return RedirectToAction("SearchProjects", "Project");
            }
            else
            {
                TempData["message"] = "Please select a project";
                return RedirectToAction("SearchProjects", "Project");
            }
        }

        [HttpGet]
        [Route("~/Document/ViewFavorDoc")]
        public IActionResult ViewFavorDoc()
        {
            return View();
        }

        [HttpPost]

        public IActionResult AddFavoriteDoc()
        {
            var docId = Int32.Parse(Request.Form["docId"].ToString());
            if (_context.FavorDocs.Where(p => p.DocumentId == docId).FirstOrDefault() == null)
            {
                var doc = new FavorDoc() { DocumentId = docId };
                _context.FavorDocs.Add(doc);
                _context.SaveChanges();
                TempData["message"] = "Added favorite document successfully!!";
            }
            else
            {
                TempData["message"] = "There is no document added or the document was already your favorite!!";
            }
            var projId = Request.Form["projId"].ToString();
            var url = "~/Project/ViewProject/" + projId;
            return Redirect(url);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDoc()
        {
            var docId = Int32.Parse(Request.Form["docId"].ToString());
            var projectId = Int32.Parse(Request.Form["projId"].ToString());
            AmazonS3Client s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.CACentral1);

            Project project = new Project();

            List<DocumentFactorRel> docFacRels = new List<DocumentFactorRel>();

            docFacRels = await _context.DocumentFactorRels.Where(d => d.DocumentFk == docId).ToListAsync();
            if (docFacRels.FirstOrDefault() != null)
            {
                foreach (var docFacRel in docFacRels)
                {
                    _context.DocumentFactorRels.Remove(docFacRel);
                    await _context.SaveChangesAsync();
                }
            }

            Document doc = await _context.Documents.FindAsync(docId);
            if (doc != null)
            {
                try
                {
                    DeleteObjectRequest request = new DeleteObjectRequest
                    {
                        BucketName = AWS_bucketName,
                        Key = doc.Url
                    };
                    await s3Client.DeleteObjectAsync(request);
                }
                catch (AmazonS3Exception ex)
                {
                    TempData["message"] = ex.Message;
                }

                _context.Documents.Remove(doc);
                await _context.SaveChangesAsync();
            }

            TempData["message"] = "Deleted the document successfully!!";
            var url = "~/Project/ViewProject/" + projectId.ToString();
            return Redirect(url);
        }
    }
}