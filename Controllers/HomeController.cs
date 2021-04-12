using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models;
using ProjectManagementCollection.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManagementCollection.Controllers
{
    public class HomeController : Controller
    {
        public static int current_role = 0;
        private readonly ILogger<HomeController> _logger;
        private readonly PmcAppDbContext _context;

        public HomeController(PmcAppDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("~/")]
        [Route("~/Login")]
        [Route("~/Home/Login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
        [Route("~/Home/Export")]
        public IActionResult Export()
        {
            return Export();
        }
        
        
        [HttpPost]
        [Route("~/")]
        [Route("~/Home")]
        [Route("~/Home/Login")]
        public IActionResult Login(LoginModel model)
        {
            /*
            User user;
            try
            {
                user = _context.Users.Where(u => u.Email == model.Email).Where(u=>u.Password == model.Password).Single();
            } catch (Exception ex)
            {
                _logger.LogInformation("Did not find User", ex);
                model.error = true;
                return View("Login",model);
            }

            if (user != null)
            {
                current_role = user.PermissionLevel;
                return RedirectToAction("SearchProjects", "Project");
            }
            else
            {
                _logger.LogInformation("Did not find the User or Password is wrong!");
                return View("Login");
            }
            */
            current_role = 1;

            return RedirectToAction("SearchProjects", "Project");
        }

        public IActionResult Logout()
        {
            current_role = 0;
            return View("Login");
        }

        [HttpPost]
        public IActionResult Export(Export project) {

            /*
            List<Project> projects = _context.Projects.Where(c => c.Success == "Yes").ToList();

            foreach (var proj in projects)
            {

                List<Document> documents = _context.Documents.Where(d => d.ProjectFk == proj.ProjectId).ToList();


                List<DocumentFactorRel> projFactors = _context.DocumentFactorRels.Where(c => c.ProjectFk == proj).ToList();
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Factors");
                    foreach (var proj in projFactors)
                    {
                        sb.AppendLine($"{projects.FactorFk}");
                    }
                    return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "project.csv");
                }
                catch
                {
                }
            
            }
            */

            return View();
        }


    }
}
