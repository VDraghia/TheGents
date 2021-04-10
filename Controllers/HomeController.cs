using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
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
            /*
            User user;
            try
            {
                user = _context.Users.Where(u => u.Email == model.Email).Single();
            } catch (Exception ex)
            {
                _logger.LogInformation("Did not find User", ex);
                return View("Login");
            }

            if (user != null && user.Password == model.Password)
            {
                return RedirectToAction("SearchProjects", "Project");
            }
            
            */
            //return View("Login");
            return RedirectToAction("SearchProjects", "Project");
        }
    }
}
