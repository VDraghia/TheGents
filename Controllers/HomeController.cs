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

        [HttpPost]
        [Route("~/")]
        [Route("~/Home")]
        [Route("~/Home/Login")]
        public IActionResult Login(Boolean logout)
        {

            if (logout)
            {
                _logger.LogWarning("User Logged out");
                return View();
            }

            return View("Search");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Error page");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
