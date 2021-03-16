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

        [Route("~/Project/ViewProject/{id}")]
        public IActionResult ViewProject(int id)
        {
            //Get the project by id
            Project project = _context.Projects.Where(c => c.ProjectId == id).Single();

            project.Documents = _context.Documents.Where(d => d.ProjectFk == id).ToList();



            List<Factor> factors = new List<Factor>();

            foreach (Document doc in project.Documents)
            {
                //Get the document factor relationships
                List<DocumentFactorRel> docFactors = _context.DocumentFactorRels.Where(c => c.DocumentFk == doc.DocumentId).ToList();
                // Get the Factors related to the Projects
                foreach (DocumentFactorRel docFac in docFactors)
                {
                    factors.Add(_context.Factors.Single(c => c.FactorId == docFac.FactorFk));
                }
            }

            Dictionary<string, string> factorDescriptions = new Dictionary<string, string>();

            //Get Main and Sub Categories for description
            foreach (Factor factor in factors)
            {
                FactorMainCategory value = _context.FactorMainCategories.Single(c => c.FactorMainCategoryId == factor.FactorMainCategoryFk);
                FactorSubCategory key = _context.FactorSubCategories.Single(c => c.FactorSubCategoryId == factor.FactorSubCategoryFk);
                //donot add if another document already has the same factor
                if (!factorDescriptions.ContainsKey(key.FactorSubCategoryDesc))
                    factorDescriptions.Add(key.FactorSubCategoryDesc, value.FactorMainCategoryDesc);

            }

            project.FactorStrings = factorDescriptions.OrderBy(o => o.Value).ToDictionary(o => o.Key, p => p.Value);


            return View(project);
        }
        [Route("~/Project/ViewProjInfo/{id}")]
        public IActionResult ViewProjInfo(int id)
        {
            //Get the project by id
            Project project = _context.Projects.Where(c => c.ProjectId == id).Single();

            project.Documents = _context.Documents.Where(c => c.ProjectFk == id).ToArray();

            List<Factor> factors = new List<Factor>();

            foreach (Document doc in project.Documents)
            {
                //Get the document factor relationships
                List<DocumentFactorRel> docFactors = _context.DocumentFactorRels.Where(c => c.DocumentFk == doc.DocumentId).ToList();
                // Get the Factors related to the Projects
                foreach (DocumentFactorRel docFac in docFactors)
                {
                    factors.Add(_context.Factors.Single(c => c.FactorId == docFac.FactorFk));
                }
            }

            Dictionary<string, string> factorDescriptions = new Dictionary<string, string>();

            //Get Main and Sub Categories for description
            foreach (Factor factor in factors)
            {
                FactorMainCategory value = _context.FactorMainCategories.Single(c => c.FactorMainCategoryId == factor.FactorMainCategoryFk);
                FactorSubCategory key = _context.FactorSubCategories.Single(c => c.FactorSubCategoryId == factor.FactorSubCategoryFk);
                //donot add if another document already has the same factor
                if (!factorDescriptions.ContainsKey(key.FactorSubCategoryDesc))
                    factorDescriptions.Add(key.FactorSubCategoryDesc, value.FactorMainCategoryDesc);

            }

            project.FactorStrings = factorDescriptions.OrderBy(o => o.Value).ToDictionary(o => o.Key, p => p.Value);

            return View(project);
        }
    }
}
