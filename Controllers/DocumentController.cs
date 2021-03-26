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
        string AWS_bucketName = "gentsproject2";

        public DocumentController(PmcAppDbContext context, ILogger<DocumentController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult SearchDocuments()
        {
            SearchDocumentModel viewModel = new SearchDocumentModel();
            //Get all factors and categories for description
            IList<Factor> factors = _context.Factors.ToList();
            IList<FactorMainCategory> mainCategories = _context.FactorMainCategories.ToList();
            IList<FactorSubCategory> subCategories = _context.FactorSubCategories.ToList();

            IList<ListFactorDescriptorModel> listFactors = new List<ListFactorDescriptorModel>();

            //Build Factor descriptor list to display
            foreach (var fac in factors)
            {
                //Get Category description
                FactorMainCategory mainDesc = mainCategories.Where(c => c.FactorMainCategoryId == fac.FactorMainCategoryFk).Single();
                FactorSubCategory subDesc = subCategories.Where(c => c.FactorSubCategoryId == fac.FactorSubCategoryFk).Single();

                //Build new List factor descriptor
                ListFactorDescriptorModel newListModel = new ListFactorDescriptorModel()
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
        public async Task<IActionResult> SearchDocuments(SearchDocumentModel modelFromView)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            // Model for view
            SearchDocumentModel docModel = new SearchDocumentModel();

            docModel.Documents = _context.Documents.Where(c => c.Name.Contains(modelFromView.DocumentName)).ToList();

            // Add Must Have Factors if any
            if (modelFromView.MustHaveFactors != null && modelFromView.MustHaveFactors.Count() > 0)
            {

                for (int i = 0; i < modelFromView.MustHaveFactors.Count(); i++)
                {
                    List<Document> temp = new List<Document>();
                    foreach (var doc in docModel.Documents)
                    {
                        if (_context.DocumentFactorRels.Where(dr => dr.DocumentFk == doc.DocumentId).Where(dr => dr.FactorFk == modelFromView.MustHaveFactors.ElementAt(i)).Count() == 1)
                        {
                            temp.Add(doc);
                        }
                    }
                    docModel.Documents = temp;
                }
            }

            // Add Must Have Factors if any
            if (modelFromView.NotHaveFactors != null && modelFromView.NotHaveFactors.Count() > 0)
            {

                for (int i = 0; i < modelFromView.NotHaveFactors.Count(); i++)
                {
                    List<Document> temp = new List<Document>();
                    foreach (var doc in docModel.Documents)
                    {
                        if (_context.DocumentFactorRels.Where(dr => dr.DocumentFk == doc.DocumentId).Where(dr => dr.FactorFk == modelFromView.NotHaveFactors.ElementAt(i)).Count() != 1)
                        {
                            temp.Add(doc);
                        }
                    }
                    docModel.Documents = temp;
                }
            }


            // Run query
            // _context.Documents.FromSqlRaw(queryDocuments).ToListAsync();
            //docModel.Documents = await _context.Documents.FromSqlRaw(queryDocuments).ToListAsync();

            //// Get All Project
            //IList<Project> projects = await _context.Projects.ToListAsync();

            //// Create Project id:name relation
            //foreach(var proj in projects)
            //{
            //    docModel.ProjectNames.Add(proj.ProjectId, proj.Name);
            //}
            //Get all factors and categories for description
            IList<Factor> factors = await _context.Factors.ToListAsync();
            IList<FactorMainCategory> mainCategories = _context.FactorMainCategories.ToList();
            IList<FactorSubCategory> subCategories = _context.FactorSubCategories.ToList();

            IList<ListFactorDescriptorModel> listFactors = new List<ListFactorDescriptorModel>();

            //Build Factor descriptor list to display
            foreach (var fac in factors)
            {
                //Get Category description
                FactorMainCategory mainDesc = mainCategories.Where(c => c.FactorMainCategoryId == fac.FactorMainCategoryFk).Single();
                FactorSubCategory subDesc = subCategories.Where(c => c.FactorSubCategoryId == fac.FactorSubCategoryFk).Single();

                //Build new List factor descriptor
                ListFactorDescriptorModel newListModel = new ListFactorDescriptorModel()
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

        //[HttpGet]
        //[Route("~/Document/Upload")]
        //[Route("~/Document/Upload/{id}")]
        //public IActionResult Upload([FromRoute] int? id)
        //{

        //    UploadDocumentModel uploadModel = new UploadDocumentModel();

        //    Project proj = new Project();

        //    // Return Project Name
        //    if (id != null) {
        //        // clear the model ProjectId
        //        //uploadModel.ProjectId = (int)id;
        //        proj = _context.Projects.Where(p => p.ProjectId == id).Single();
        //    }

        //    // Populate text field with empty string
        //    if(proj.ProjectId == 0)
        //    {
        //        uploadModel.ProjectName = "";
        //    }

        //    uploadModel.ProjectName = proj.Name;

        //    //Get all factors and categories for description
        //    IList<Factor> factors = _context.Factors.ToList();
        //    IList<FactorMainCategory> mainCategories = _context.FactorMainCategories.ToList();
        //    IList<FactorSubCategory> subCategories = _context.FactorSubCategories.ToList();

        //    IList<ListFactorDescriptorModel> listFactors = new List<ListFactorDescriptorModel>();

        //    //Build Factor descriptor list to display
        //    foreach(var fac in factors)
        //    {
        //        //Get Category description
        //        FactorMainCategory mainDesc = mainCategories.Where(c => c.FactorMainCategoryId == fac.FactorMainCategoryFk).Single();
        //        FactorSubCategory subDesc = subCategories.Where(c => c.FactorSubCategoryId == fac.FactorSubCategoryFk).Single();

        //        //Build new List factor descriptor
        //        ListFactorDescriptorModel newListModel = new ListFactorDescriptorModel()
        //        {
        //            FactorId = fac.FactorId,
        //            Position = fac.Position,
        //            MainCategoryDesc = mainDesc.FactorMainCategoryDesc,
        //            SubCategoryDesc = subDesc.FactorSubCategoryDesc
        //        };

        //        listFactors.Add(newListModel);
        //    }

        //    /*
        //     * Order factors by position number to place them in sequential category
        //     * This simplifies displaying on page.
        //     */
        //    uploadModel.ListFactorDesc = listFactors.OrderBy(f => f.Position).ToList();

        //    return View(uploadModel);
        //}



        [HttpGet]
        [Route("~/Document/Upload")]
        [Route("~/Document/Upload/{id}")]
        public async Task<IActionResult> Upload([FromRoute] int? id)
        {

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

            IList<ListFactorDescriptorModel> listFactors = new List<ListFactorDescriptorModel>();

            //Build Factor descriptor list to display
            foreach (var fac in factors)
            {
                //Get Category description
                FactorMainCategory mainDesc = mainCategories.Where(c => c.FactorMainCategoryId == fac.FactorMainCategoryFk).Single();
                FactorSubCategory subDesc = subCategories.Where(c => c.FactorSubCategoryId == fac.FactorSubCategoryFk).Single();

                //Build new List factor descriptor
                ListFactorDescriptorModel newListModel = new ListFactorDescriptorModel()
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

            UploadDocumentModel modelToView = new UploadDocumentModel();

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

                string newKeyName = existingProject.Name + "/" + uploadFile.FileName;
                var fs = uploadFile.OpenReadStream();

                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = AWS_bucketName,
                    Key = newKeyName,
                    InputStream = fs,
                    ContentType = uploadFile.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                await s3Client.PutObjectAsync(request);


                string result = string.Format("http://{0}.s3.amazonaws.com/{1}", AWS_bucketName, newKeyName);

                //Create new document
                Document doc = new Document()
                {
                    Name = uploadFile.FileName,
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

            // Empty the project name string before returning to upload page
            //modelToView.ProjectName = "";


            //IList<Factor> factors = _context.Factors.ToList();
            //IList<FactorMainCategory> mainCategories = _context.FactorMainCategories.ToList();
            //IList<FactorSubCategory> subCategories = _context.FactorSubCategories.ToList();

            //IList<ListFactorDescriptorModel> listFactors = new List<ListFactorDescriptorModel>();

            ////Build Factor descriptor list to display
            //foreach (var fac in factors)
            //{
            //    //Get Category description
            //    FactorMainCategory mainDesc = mainCategories.Where(c => c.FactorMainCategoryId == fac.FactorMainCategoryFk).Single();
            //    FactorSubCategory subDesc = subCategories.Where(c => c.FactorSubCategoryId == fac.FactorSubCategoryFk).Single();

            //    //Build new List factor descriptor
            //    ListFactorDescriptorModel newListModel = new ListFactorDescriptorModel()
            //    {
            //        FactorId = fac.FactorId,
            //        Position = fac.Position,
            //        MainCategoryDesc = mainDesc.FactorMainCategoryDesc,
            //        SubCategoryDesc = subDesc.FactorSubCategoryDesc
            //    };

            //    listFactors.Add(newListModel);
            //}

            ///*
            // * Order factors by position number to place them in sequential category
            // * This simplifies displaying on page.
            // */
            //modelToView.ListFactorDesc = listFactors.OrderBy(f => f.Position).ToList();

            return RedirectToAction("Upload", "Document");
            //return View(modelToView);
        }


        [Route("~/Document/ViewDocument/{id}")]
        public IActionResult ViewDocument(int id)
        {
            ViewDocumentModel viewModel = new ViewDocumentModel();
            //Get the project by id
            viewModel.Document = _context.Documents.Where(c => c.DocumentId == id).Single();


            List<Factor> factors = new List<Factor>();

            //Get the document factor relationships
            List<DocumentFactorRel> docFactors = _context.DocumentFactorRels.Where(c => c.DocumentFk == viewModel.Document.DocumentId).ToList();
            // Get the Factors related to the Projects
            foreach (DocumentFactorRel docFac in docFactors)
            {
                factors.Add(_context.Factors.Single(c => c.FactorId == docFac.FactorFk));
            }

            Dictionary<string, string> factorDescriptions = new Dictionary<string, string>();

            //Get Main and Sub Categories for description
            foreach (Factor factor in factors)
            {
                FactorMainCategory value = _context.FactorMainCategories.Single(c => c.FactorMainCategoryId == factor.FactorMainCategoryFk);
                FactorSubCategory key = _context.FactorSubCategories.Single(c => c.FactorSubCategoryId == factor.FactorSubCategoryFk);
                //donot add if another document already has the same factor
                if (!factorDescriptions.ContainsKey(key.FactorSubCategoryDesc))
                    factorDescriptions.Add(key.FactorSubCategoryDesc, value.FactorMainCategoryDesc);

            }

            viewModel.FactorStrings = factorDescriptions.OrderBy(o => o.Value).ToDictionary(o => o.Key, p => p.Value);


            return View(viewModel);
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
                TempData["link"] = url;
                return RedirectToAction("ViewFavorDoc", "Document");
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
            var url = "~/Project/ViewProjInfo/" + projId;
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
            var url = "~/Project/ViewProjInfo/" + projectId.ToString();
            return Redirect(url);
        }

    }
}
