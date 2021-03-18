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
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var viewModel = new SearchProjectModel();
            viewModel.AllProjects = _context.Projects.FromSqlRaw("SELECT * FROM dbo.Projects").ToList();
            viewModel.FavorProjs = _context.FavorProjs.FromSqlRaw("SELECT * FROM dbo.FavorProjs").ToList();

            var aList = new List<SelectListItem>();
            foreach (var proj in viewModel.FavorProjs)
            {
                var projName = viewModel.AllProjects.Where(p => p.ProjectId == proj.ProjectId).Single().Name;
                aList.Add(new SelectListItem { Text = projName, Value = proj.Url });
            }
            ViewData["Projects"] = aList;


            viewModel.Documents = _context.Documents.FromSqlRaw("SELECT * FROM dbo.Documents").ToList();
            viewModel.FavorDocs = _context.FavorDocs.FromSqlRaw("SELECT * FROM dbo.FavorDocs").ToList();

            var bList = new List<SelectListItem>();
            foreach (var doc in viewModel.FavorDocs)
            {
                var docName = viewModel.Documents.Where(p => p.DocumentId == doc.DocumentId).Single().Url;
                var url = "https://gentsproject2.s3.ca-central-1.amazonaws.com/" + viewModel.Documents.Where(p => p.DocumentId == doc.DocumentId).Single().Url.Replace(" ", "+");
                bList.Add(new SelectListItem { Text = docName, Value = url });
            }
            ViewData["Docs"] = bList;

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

            searchProject.Projects = _context.Projects.Where(p => p.Name.Contains(searchModel.Name)).ToList();


            searchProject.AllProjects = _context.Projects.FromSqlRaw("SELECT * FROM dbo.Projects").ToList();
            searchProject.FavorProjs = _context.FavorProjs.FromSqlRaw("SELECT * FROM dbo.FavorProjs").ToList();

            var aList = new List<SelectListItem>();
            foreach (var proj in searchProject.FavorProjs)
            {
                var projName = searchProject.AllProjects.Where(p => p.ProjectId == proj.ProjectId).Single().Name;
                aList.Add(new SelectListItem { Text = projName, Value = proj.Url });
            }
            ViewData["Projects"] = aList;

            searchProject.Documents = _context.Documents.FromSqlRaw("SELECT * FROM dbo.Documents").ToList();
            searchProject.FavorDocs = _context.FavorDocs.FromSqlRaw("SELECT * FROM dbo.FavorDocs").ToList();

            var bList = new List<SelectListItem>();
            foreach (var doc in searchProject.FavorDocs)
            {
                var docName = searchProject.Documents.Where(p => p.DocumentId == doc.DocumentId).Single().Url;
                var url = "https://gentsproject2.s3.ca-central-1.amazonaws.com/" + searchProject.Documents.Where(p => p.DocumentId == doc.DocumentId).Single().Url.Replace(" ", "+");
                bList.Add(new SelectListItem { Text = docName, Value = url });
            }
            ViewData["Docs"] = bList;



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
                }
                else
                {
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
                }
                else
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

        [HttpPost]
        public IActionResult FavoriteProj()
        {
            var url = Request.Form["urlproj"].ToString();
            var command = Request.Form["cmdProj"].ToString();
            if (command.Equals("Display") && !url.Equals(""))
            {
                return Redirect(url);
            }
            else if (command.Equals("Remove") && !url.Equals(""))
            {
                FavorProj proj = _context.FavorProjs.Where(p => p.Url == url).Single();
                _context.FavorProjs.Remove(proj);
                _context.SaveChanges();
                TempData["message"] = "Remove the project successfully!!";
                return RedirectToAction("SearchProjects", "Project");
            }
            else
            {
                TempData["message"] = "Please select a project";
                return RedirectToAction("SearchProjects", "Project");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteProj(int projectId)
        {

            //FavorProj proj = _context.FavorProjs.Where(p => p.Url == url).Single();
            var url = "~/Project/ViewProject/" + projectId.ToString();
            if (_context.FavorProjs.Where(p => p.ProjectId == projectId).FirstOrDefault() == null)
            {
                var proj = new FavorProj() { ProjectId = projectId, Url = url };
                _context.FavorProjs.Add(proj);
                await _context.SaveChangesAsync();
                TempData["message"] = "Add favorite project successfully!!";
            }
            else
            {
                TempData["message"] = "There is no project added or the project was already your favorite!!";
            }
            return Redirect(url);
        }


    }
}