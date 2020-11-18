using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementCollection.Controllers
{
    public class SearchController : Controller
    {

        private readonly ILogger<SearchController> _logger;
        private readonly PmcAppDbContext _context;

        public SearchController(PmcAppDbContext context, ILogger<SearchController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [Route("~/Search/Search")]
        public IActionResult Search(IEnumerable<Project> projects)
        {

            return View(projects);
        }

    }
}
