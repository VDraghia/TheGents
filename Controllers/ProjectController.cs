using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Controllers
{
    public class ProjectController : Controller
    {

        private readonly ILogger<ProjectController> _logger;
        private readonly PmcAppDbContext _context;


        public ProjectController(PmcAppDbContext context, ILogger<ProjectController> logger)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult SearchProject(string projectName)
        {

            if(projectName == "")
            {
                return View();
            }

            List<Project> project = _context.Projects.Where(p => p.Name.Contains(projectName)).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Location,Client,Completed")] Project project)
        {

            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return View();
            }
            //List<Project> possibleProjects = from projects in _context.Set<Projects>

            return View(project);
        }
    }
}
