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

        [HttpGet]
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
            // Null both fields if either is null
            if (searchModel.DateRangeMin == null || searchModel.DateRangeMin == null)
            {
                searchModel.DateRangeMin = null;
                searchModel.DateRangeMax = null;
            }

            string query = @"SELECT * FROM dbo.Projects WHERE Success == @Success ";

            //Build Parameter List
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Success", searchModel.Success));

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                parameters.Add(new SqlParameter("@Name", searchModel.Name));
                query += "AND Name == @Name ";
            }

            if (!string.IsNullOrEmpty(searchModel.Uploader))
            {
                parameters.Add(new SqlParameter("@Uploader", searchModel.Uploader));
                query += "AND Uploader == @Uploader ";
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
                query += "AND Client == @Client ";
            }

            if (!string.IsNullOrEmpty(searchModel.Location))
            {
                parameters.Add(new SqlParameter("@Location", searchModel.Location));
                query += "AND Location == @Location ";
            }

            var projects = _context.Projects.ToList();

            IEnumerable<Project> listed = projects;

            //ViewBag["Projects"] = listed.ToList();

            return RedirectToAction("Search", "Search", listed);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Error page");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
