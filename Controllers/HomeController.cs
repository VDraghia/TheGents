using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using Azure;
using ProjectManagementCollection.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

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

        // by tim
        private static Login _login = new Login();
        private static string resProject = "";
        private static int projId = 0;

        [HttpPost]
        [Route("~/")]
        [Route("~/Home")]
        [Route("~/Home/Login")]
        public IActionResult Login(Boolean logout, Login login)
        {
            _login = login;
            if (logout)
            {
                _logger.LogWarning("User Logged out");
                return View();
            }
            // tim test login user, just test if email is in database
            if (_context.Users.FirstOrDefault(p => p.Email == login.Email) == null)
            {
                ViewBag.msg = "User not exist";
                return View();
            }
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
        public IActionResult Search(Search searchModel)
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
            string query = @"SELECT * FROM dbo.Projects WHERE ";

            //Build Parameter List
            List<SqlParameter> parameters = new List<SqlParameter>();

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

            return View(searchModel);
        }


        [Route("~/Home/ViewDocument/{id}")]
        public IActionResult ViewDocument(int id)
        {
            //Get the project by id
            Project project = _context.Projects.Where(c => c.ProjectId == id).Single();

            //Get the project factor relationships
            List<ProjectFactorRel> projFactors = _context.ProjectFactorRels.Where(c => c.ProjectFk == id).ToList();

            List<Factor> factors = new List<Factor>();

            // Get the Factors related to the Projects
            foreach (ProjectFactorRel projFac in projFactors)
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

            project.Factors = factorDescriptions;

            project.Documents = _context.Documents.Where(d => d.ProjectDocFk == id).ToList();

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
            var viewModel = new UserProject();
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

            return View(viewModel);
        }

        //by Tim
        string AWS_accessKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_accessKey"];
        string AWS_secretKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_secretKey"];
        string AWS_bucketName = "gentsproject2";
        //string AWS_defaultFolder = "MyTest_Folder";

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
