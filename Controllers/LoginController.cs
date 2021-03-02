using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        [Route("~/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("~/Login")]
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


    }
}
