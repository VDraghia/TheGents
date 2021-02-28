using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Amazon.S3;
using ProjectManagementCollection.Models.ViewModels;


namespace ProjectManagementCollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PmcAppDbContext _context;


        public HomeController(PmcAppDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [Route("~/")]
        [Route("~/Home")]
        [Route("~/Home/Login")]
        public IActionResult Login()
        {
            return View();
        }

       /*
        private static ViewDocumentViewModel _login = new ViewDocumentViewModel();
        private static string resProject = "";
        private static int projId = 0;
       */

        [HttpPost]
        [Route("~/")]
        [Route("~/Home")]
        [Route("~/Home/Login")]
        public IActionResult Login(Boolean logout)
        {
            /*
            _login = login;
            if (logout)
            {
                _logger.LogWarning("User Logged out");
                return View();
            }
            */

            //return View("Search");
            return RedirectToAction("Search");
        }

        [HttpGet]
        [Route("~/Home/Search")]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [Route("~/Home/Search")]
        public IActionResult Search(SearchViewModel searchModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Null both fields if either is null
            if (searchModel.DateRangeMin == null || searchModel.DateRangeMin == null)
            {
                searchModel.DateRangeMin = null;
                searchModel.DateRangeMax = null;
            }

            //Start query string
            string query = @"SELECT * FROM dbo.Projects WHERE 1=1";

            

            //Build Parameter List
            List<SqlParameter> parameters = new List<SqlParameter>();

            /*
            //Check if searching for success and failure of project
            if (searchModel.Success.Equals("Both"))
            {
                query += "(Success = @Success OR Success = @Success2) ";
                parameters.Add(new SqlParameter("@Success", "Yes"));
                parameters.Add(new SqlParameter("@Success2", "No"));
            }
            else
            {
                query += "Success = @Success ";
                parameters.Add(new SqlParameter("@Success", searchModel.Success));
            }
            */


            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                parameters.Add(new SqlParameter("@Name", searchModel.Name));
                query += "AND Name = @Name ";
            }

            if (!string.IsNullOrEmpty(searchModel.Uploader))
            {
                parameters.Add(new SqlParameter("@Uploader", searchModel.Uploader));
                query += "AND Uploader = @Uploader ";
            }

            if (searchModel.DateRangeMax != null)
            {
                parameters.Add(new SqlParameter("@DateRangeMin", searchModel.DateRangeMin.ToString()));
                parameters.Add(new SqlParameter("@DateRangeMax", searchModel.DateRangeMax.ToString()));
                query += "AND DateCompleted BETWEEN @DateRangeMin AND @DateRangeMax ";
            }

            if (!string.IsNullOrEmpty(searchModel.Client))
            {
                parameters.Add(new SqlParameter("@Client", searchModel.Client));
                query += "AND Client = @Client ";
            }

            if (!string.IsNullOrEmpty(searchModel.Location))
            {
                parameters.Add(new SqlParameter("@Location", searchModel.Location));
                query += "AND Location = @Location ";
            }

            searchModel.Projects = _context.Projects.FromSqlRaw(query, parameters.ToArray()).ToList();


            //testing search by factor
            Factor testFac = new Factor { 
                FactorId = 1,
                FactorMainCategoryFk = 1, 
                FactorSubCategoryFk = 2, 
                Position = 1, 
                FactorDesc = "desc" 
            };

            List<Factor> factorList = new List<Factor>();
            factorList.Add(testFac);

            // Factor list populated from 
            searchModel.Factors = factorList;

            if (searchModel.Factors.Any()) {

                foreach (var factor in searchModel.Factors)
                {
                    var docs = from document in _context.Set<Document>()
                               join documentFactorRel in _context.Set<DocumentFactorRel>()
                                   on document.DocumentId equals documentFactorRel.DocumentFk
                               join factors in _context.Set<Factor>()
                                   on documentFactorRel.FactorFk equals factors.FactorId
                               where factors.FactorId == factor.FactorId
                                select new { document };                      
                }
            }
            return View(searchModel);
        }



         [Route("~/Home/ViewDocument/{id}")]
        public IActionResult ViewDocument(int id)
        {
            //Get the project by id
            Project project = _context.Projects.Where(c => c.ProjectId == id).Single();

            //Get the project factor relationships
            List<DocumentFactorRel> projFactors = _context.DocumentFactorRels.Where(c => c.DocumentFk == id).ToList();


            List<Factor> factors = new List<Factor>();

            // Get the Factors related to the Projects
            foreach (DocumentFactorRel projFac in projFactors)
            {
                factors.Add(_context.Factors.Single(c => c.FactorId == projFac.FactorFk));
            }

            Dictionary<string, string> factorDescriptions = new Dictionary<string, string>();

            //Get Main and Sub Categories for description
            foreach (Factor factor in factors)
            {
                FactorMainCategory value = _context.FactorMainCategories.Single(c => c.FactorMainCategoryId == factor.FactorMainCategoryFk);
                FactorSubCategory key = _context.FactorSubCategories.Single(c => c.FactorSubCategoryId == factor.FactorSubCategoryFk);

                factorDescriptions.Add(key.FactorSubCategoryDesc, value.FactorMainCategoryDesc);

            }
 
            /*
            List<Document> documents = new List<Document>();
            if(project.Name == "Project1")
                documents.Add(new Document { DocumentId = 1, Name = "Project1", Url = @"/pdf/project1.pdf", ProjectFk = 1 });
            if (project.Name == "Project2")
                documents.Add(new Document { DocumentId = 2, Name = "Project2", Url = @"/pdf/project2.pdf", ProjectFk = 1 });
            if (project.Name == "Project3")
                documents.Add(new Document { DocumentId = 3, Name = "Project3", Url = @"/pdf/project3.pdf", ProjectFk = 2 });

            project.Documents = documents; 

            */

            return View(project);
        }

        //by tim
        [Route("~/Home/ViewProjInfo/{id}")]
        public IActionResult ViewProjInfo(int id)
        {
            //Get the project by id
            Project project = _context.Projects.Where(c => c.ProjectId == id).Single();

            project.Documents = _context.Documents.Where(c => c.ProjectDocFk == id).ToArray();

            //Get the project factor relationships
            List<ProjectFactorRel> projFactors = _context.ProjectFactorRels.Where(c => c.ProjectFk == id).ToList();

            List<Factor> factors = new List<Factor>();

            // Get the Factors related to the Projects
            foreach (ProjectFactorRel projFac in projFactors)
            {
                factors.Add(_context.Factors.Single(c => c.FactorId == projFac.FactorFk));
            }
            ViewData["Projects"] = aList;
            // pass selected object Name and ProjectId to view
            ViewData["ProjectName"] = resProject;
            viewModel.Project = await _context.Projects.FirstOrDefaultAsync(m => m.Name == resProject);
            ViewData["ProjectId"] = viewModel.Project.ProjectId;
            
            return View();
            

        }

            Dictionary<string, string> factorDescriptions = new Dictionary<string, string>();

            //Get Main and Sub Categories for description
            foreach (Factor factor in factors)
            {
                FactorMainCategory value = _context.FactorMainCategories.Single(c => c.FactorMainCategoryId == factor.FactorMainCategoryFk);
                FactorSubCategory key = _context.FactorSubCategories.Single(c => c.FactorSubCategoryId == factor.FactorSubCategoryFk);

                factorDescriptions.Add(key.FactorSubCategoryDesc, value.FactorMainCategoryDesc);

            }

            project.Factors = factorDescriptions;

            return View(project);
        }

        public async Task<IActionResult> UploadF()
        {
            /*
            var viewModel = new UserProjectViewModel();
            viewModel.User = await _context.Users.FirstOrDefaultAsync(u => u.Email == _login.Email);

            viewModel.Projects = await _context.Projects.ToListAsync();

            viewModel.FactorSubCategories = await _context.FactorSubCategories.ToListAsync();

            List<Factor> factors = await _context.Factors.ToListAsync();

            // prepare for creating a dictionary have pair values: FactorSubCategoryDesc and FactorId which will be listed in view
            Dictionary<int, Tuple<string, string>> factorDescriptions = new Dictionary<int, Tuple<string, string>>();

            //Get Sub Categories for description
            foreach (Factor factor in factors)
            {

                FactorSubCategory key = _context.FactorSubCategories.Single(c => c.FactorSubCategoryId == factor.FactorSubCategoryFk);
                FactorMainCategory value = _context.FactorMainCategories.Single(x => x.FactorMainCategoryId == factor.FactorMainCategoryFk);

                // prepare for: the FactorId will be saved to database if the factor is checked
                factorDescriptions.Add(factor.FactorId, new Tuple<string, string>(value.FactorMainCategoryDesc, key.FactorSubCategoryDesc));
            }


            // the Factors in viewModel is a dictioary
            viewModel.Factors = factorDescriptions;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectCreate([Bind("ProjectId,Name,Uploaded,DateCompleted,Client,Location,Success,Uploader_id")] Project project)
        {
            /*
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                resProject = project.Name;
                return RedirectToAction("Upload");
            }
            */
            return RedirectToAction("ProjectDetail");
        }

        //by Tim
        string AWS_accessKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_accessKey"];
        string AWS_secretKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_secretKey"];
        string AWS_bucketName = "gentsproject2";


        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        public async Task<IActionResult> UploadF(List<IFormFile> uploadFile, Project project, UserProject userProject)
        {

            ViewBag.result = await UploadFToAWSAsync(uploadFile, project, userProject);
            return RedirectToAction("UploadF");
        }

        protected async Task<string> UploadFToAWSAsync(List<IFormFile> uploadFile, Project project, UserProject userProject)
        {
            var result = "";
            Project proj = new Project();
            proj = await _context.Projects.FirstOrDefaultAsync(p => p.Name == project.Name);
            if (proj == null)
            {
                TempData["message"] = "The project is not existed, Upload failed!!";
                return result;
            }
            projId = proj.ProjectId;
            var subFolder = project.Name;

            try
            {
                var s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.CACentral1);
                var bucketName = AWS_bucketName;
                var keyName = "";
                if (!string.IsNullOrEmpty(subFolder))
                    keyName = subFolder.Trim();
                foreach (var uFile in uploadFile)
                {
                    var newKeyName = keyName + "/" + uFile.FileName;
                    var fs = uFile.OpenReadStream();
                    var request = new Amazon.S3.Model.PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = newKeyName,
                        InputStream = fs,
                        ContentType = uFile.ContentType,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    await s3Client.PutObjectAsync(request);

                    result = string.Format("http://{0}.s3.amazonaws.com/{1}", bucketName, newKeyName);

                    var file = new Document()
                    {
                        Name = uFile.FileName,
                        Url = newKeyName,
                        ProjectDocFk = projId
                    };

                    _context.Documents.Add(file);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "Uploaded Successfully!!";

                    var aa = userProject.AreChecked;
                    if (aa != null)
                    {
                        foreach (int a in aa)
                        {
                            var row = _context.ProjectFactorRels.FirstOrDefault(p => p.FactorFk == a && p.ProjectFk == projId);
                            if (row == null)
                            {
                                _context.ProjectFactorRels.Add(new ProjectFactorRel { FactorFk = a, ProjectFk = projId });
                                _context.SaveChanges();
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Error page");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
