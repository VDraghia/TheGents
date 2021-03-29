using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models;
using ProjectManagementCollection.Models.ViewModels;
using System;
using System.Linq;


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

        [HttpPost]
        [Route("~/")]
        [Route("~/Home")]
        [Route("~/Home/Login")]
        public IActionResult Login(LoginModel model)
        {
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
        }
        public IActionResult Logout()
        {
            current_role = 0;
            return View("Login");;
        }
    }
}
