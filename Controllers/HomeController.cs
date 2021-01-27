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
            if(!ModelState.IsValid)
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
            } else
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

            foreach(Factor factor in factors)
            {
                FactorMainCategory value = _context.FactorMainCategories.Single(c => c.FactorMainCategoryId == factor.FactorMainCategoryFk);
                FactorSubCategory key = _context.FactorSubCategories.Single(c => c.FactorSubCategoryId == factor.FactorSubCategoryFk);

                factorDescriptions.Add(key.FactorSubCategoryDesc, value.FactorMainCategoryDesc);

            }

            project.Factors = factorDescriptions;

            return View(project);
        }

        //2020-11-20 by Tim

        public async Task<IActionResult> Upload()
        {
            var viewModel = new UserProject();
            viewModel.User = await _context.Users.FirstOrDefaultAsync(u => u.Email == _login.Email);
            // need to modify the project model to match the database design
            //viewModel.Projects= await _context.Projects.Where(p =>p.Client==_login.Email).ToListAsync();
            viewModel.Projects = await _context.Projects.Where(p => p.Uploader_id == viewModel.User.UserId).ToListAsync();

            var aList = new List<SelectListItem>();
            aList.Add(new SelectListItem { Text = "New", Value = "New" });
            foreach (var project in viewModel.Projects)
            {
                aList.Add(new SelectListItem { Text = project.Name, Value = project.Name });
            }
            ViewData["Projects"] = aList;
            // pass selected object Name and ProjectId to view
            ViewData["ProjectName"] = resProject;
            viewModel.Project = await _context.Projects.FirstOrDefaultAsync(m => m.Name == resProject);
            ViewData["ProjectId"] = viewModel.Project.ProjectId;
            return View(viewModel);
        }



        public async Task<IActionResult> ProjectDetail()
        {
            var viewModel = new UserProject();
            viewModel.User = await _context.Users.FirstOrDefaultAsync(u => u.Email == _login.Email);

            viewModel.Projects = await _context.Projects.Where(p => p.Uploader_id == viewModel.User.UserId).ToListAsync();

            var aList = new List<SelectListItem>();
            aList.Add(new SelectListItem { Text = "New", Value = "New" });
            foreach (var proj in viewModel.Projects)
            {
                aList.Add(new SelectListItem { Text = proj.Name, Value = proj.Name });
            }
            ViewData["Projects"] = aList;

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> ProjectDetailConfirm()
        {
            var viewModel = new UserProject();
            viewModel.User = await _context.Users.FirstOrDefaultAsync(u => u.Email == _login.Email);
            // need to modify the project model to match the database design
            viewModel.Projects = await _context.Projects.Where(p => p.Uploader_id == viewModel.User.UserId).ToListAsync();

            var aList = new List<SelectListItem>();
            aList.Add(new SelectListItem { Text = "New", Value = "New" });
            foreach (var proj in viewModel.Projects)
            {
                aList.Add(new SelectListItem { Text = proj.Name, Value = proj.Name });
            }
            ViewData["Projects"] = aList;
            resProject = Request.Form["personDropDown"].ToString();
            ViewData["ProjectName"] = resProject;
            // if the selected option is "New", pass ViewData["ProjectName"] "New" to view to display the form for create new project; else direct to action "Upload"
            if (resProject == "New")
            {
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Upload");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectCreate([Bind("ProjectId,Name,Uploaded,DateCompleted,Client,Location,Success,Uploader_id")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                resProject = project.Name;
                return RedirectToAction("Upload");
            }
            return RedirectToAction("ProjectDetail");
        }



        //by Tim

        string AWS_accessKey = "**";
        string AWS_secretKey = "**";
        string AWS_bucketName = "**";
        string AWS_defaultFolder = "MyTest_Folder";

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile uploadFile, Project project)
        {

            ViewBag.result = await UploadFileToAWSAsync(uploadFile, project);
            return RedirectToAction("Factors");
        }

        protected async Task<string> UploadFileToAWSAsync(IFormFile uploadFile, Project project)
        {
            projId = project.ProjectId;
            var subFolder = project.Name;
            var result = "";
            try
            {
                var s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.CACentral1);
                var bucketName = AWS_bucketName;
                var keyName = AWS_defaultFolder;
                if (!string.IsNullOrEmpty(subFolder))
                    keyName = keyName + "/" + subFolder.Trim();
                keyName = keyName + "/" + uploadFile.FileName;

                var fs = uploadFile.OpenReadStream();
                var request = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    InputStream = fs,
                    ContentType = uploadFile.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };
                await s3Client.PutObjectAsync(request);

                result = string.Format("http://{0}.s3.amazonaws.com/{1}", bucketName, keyName);

                var file = new Document()
                {
                    Name = uploadFile.FileName + _login.Password,
                    Url = uploadFile.FileName,
                    ProjectDocFk = projId
                };

                _context.Documents.Add(file);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }



        //by tim
        public async Task<IActionResult> Factors()
        {
            ViewData["ProjId"] = projId;
            ViewBag.Message = "File Uploaded Successfully!!";
            var viewModel = new FactorCate();
            viewModel.FactorSubCategories = await _context.FactorSubCategories.ToListAsync();


            List<Factor> factors = await _context.Factors.ToListAsync();

            // prepare for creating a dictionary have pair values: FactorSubCategoryDesc and FactorId which will be listed in view
            Dictionary<string, int> factorDescriptions = new Dictionary<string, int>();

            //Get Sub Categories for description
            foreach (Factor factor in factors)
            {

                FactorSubCategory key = _context.FactorSubCategories.Single(c => c.FactorSubCategoryId == factor.FactorSubCategoryFk);
                // because there are two same name factors, combine the factor description with factorId.
                factorDescriptions.Add(key.FactorSubCategoryDesc + factor.FactorId.ToString(), factor.FactorId);

            }
            // the Factors in viewModel is a dictioary
            viewModel.Factors = factorDescriptions;
            return View(viewModel);
        }


        //by Tim
        [HttpPost]
        public IActionResult FactorsConfirm(FactorCate factorCate)
        {
            // the BindProperty is a int list collected the checked values from view selected by the user
            var aa = factorCate.AreChecked;
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

            return RedirectToAction("Search");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Error page");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
