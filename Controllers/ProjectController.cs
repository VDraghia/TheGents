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

        [HttpGet]
        [Route("~/Project/ViewProject/{id}")]
        public IActionResult ViewProject(int id)
        {
            //Create View Model
            ViewProjectModel model = new ViewProjectModel();

            //Get Documents related to project
            IList<Document> docs = _context.Documents.Where(d => d.ProjectFk == id).ToList();

            // Get document factor relations
            IList<DocumentFactorRel> projectFactors = new List<DocumentFactorRel>();
            foreach (Document doc in docs)
            {
                // Find the Document Factor Relation
                IList<DocumentFactorRel> docRels = _context.DocumentFactorRels.Where(f => f.DocumentFk == doc.DocumentId).ToList();

                //Add document factor relation to project factor relation
                foreach (DocumentFactorRel rel in docRels)
                {
                    projectFactors.Add(rel);
                }
            }

            //Get all factors for the project from the list of related documents
            IList<Factor> factors = new List<Factor>();
            foreach (DocumentFactorRel projFac in projectFactors)
            {
                factors.Add(_context.Factors.Where(f => f.FactorId == projFac.FactorFk).Single());
            }

            //Get all factors and categories for description
            IList<FactorMainCategory> mainCategories = _context.FactorMainCategories.ToList();
            IList<FactorSubCategory> subCategories = _context.FactorSubCategories.ToList();

            IList<ListFactorDescriptor> listFactors = new List<ListFactorDescriptor>();

            //Build Factor descriptor list to display
            foreach (var fac in factors)
            {
                //Get Category description
                FactorMainCategory mainDesc = mainCategories.Where(c => c.FactorMainCategoryId == fac.FactorMainCategoryFk).Single();
                FactorSubCategory subDesc = subCategories.Where(c => c.FactorSubCategoryId == fac.FactorSubCategoryFk).Single();

                //Build new List factor descriptor
                ListFactorDescriptor factorDescriptor = new ListFactorDescriptor()
                {
                    FactorId = fac.FactorId,
                    Position = fac.Position,
                    MainCategoryDesc = mainDesc.FactorMainCategoryDesc,
                    SubCategoryDesc = subDesc.FactorSubCategoryDesc
                };

                listFactors.Add(factorDescriptor);
            }

            listFactors.OrderBy(f => f.Position);

            listFactors = new HashSet<ListFactorDescriptor>(listFactors).ToList();

            model.SelectedProject = _context.Projects.Where(p => p.ProjectId == id).SingleOrDefault();
            model.SelectedProject.Documents = docs;
            model.FactorDescriptiors = listFactors;

            return View(model);
        }
    }
}
