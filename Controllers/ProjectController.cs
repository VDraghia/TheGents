using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models.ViewModels;
using ProjectManagementCollection.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

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


        [Route("~/")]
        [Route("~/Project/")]
        [Route("~/Project/Search")]
        public IActionResult SearchProjects()
        {
            return View();
        }

        [HttpPost]
        [Route("~/")]
        [Route("~/Project/")]
        [Route("~/Project/Search")]
        public IActionResult SearchProjects(SearchProjectModel searchModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Model for view
            SearchProjectModel searchProject = new SearchProjectModel();

            searchProject.Projects =  _context.Projects.Where(p => p.Name.Contains(searchModel.Name)).ToList();

            return View(searchProject);
        }

        [HttpGet]
        [Route("~/Project/FindCreateProject")]
        [Route("~/Project/FindCreateProject/{id}")]
        public IActionResult FindCreateProject(FindCreateProjectModel findCreateModel)
        {
            FindCreateProjectModel model = new FindCreateProjectModel();
            model.Projects = new List<Project>();

            if (findCreateModel.CreateProject)
            {
                
                if (findCreateModel.CreateProjectName != null) 
                { 
                    // Check if Project name exists
                    if (_context.Projects.Where(p => p.Name == findCreateModel.CreateProjectName).Any())
                    {
                        model.Message = "Cannot Create Project, Already Exists";
                        model.CreateProject = true;
                        return View(model);
                    }
                    else
                    {
                        Project newProject = new Project()
                        {
                            Name = findCreateModel.CreateProjectName,
                            Success = "true"
                        };

                        _context.Projects.Add(newProject);
                        _context.SaveChanges();

                        model.CreateSuccess = true;
                        model.Message = "Project created successfully!";

                        return View(model);
                    }
                } else {
                    model.Message = "Please Enter Project Name";
                    model.CreateProject = true;
                    return View(model);
                }
            }

            if (findCreateModel.FindProject)
            {
                model.FindProject = true;
                if (findCreateModel.FindProjectName != null)
                {
                    model.Projects = _context.Projects.Where(p => p.Name.Contains(findCreateModel.FindProjectName)).ToList();
                } else
                {
                    model.Message = "Please Enter Project Name";
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Export(Export project)
        {

            var query = _context.DocumentFactorRels.Select(s => s.ProjectFactor).ToArray();

            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in query)
                {

                    sb.AppendLine(item.ToString());
                    sb.AppendLine(",");

                }
                return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "project.csv");
            }
            catch
            {
                return View(project);
            }
        }

    }
}
