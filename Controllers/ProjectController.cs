using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using ProjectManagementCollection.Models.ViewModels;
using ProjectManagementCollection.Models.DescriptorModels;
using ProjectManagementCollection.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Amazon.S3;
using Amazon.S3.Model;
using System;

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

            if (HomeController.current_role == 0)
            {
                return RedirectToAction("Login", "Home");
            }
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
                var docName = viewModel.Documents.Where(p => p.DocumentId == doc.DocumentId).Single().Name;
                var url = "https://gentsproject.s3.ca-central-1.amazonaws.com/" + viewModel.Documents.Where(p => p.DocumentId == doc.DocumentId).Single().Url.Replace(" ", "+");
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
            if (HomeController.current_role == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Model for view
            SearchProjectModel searchProject = new SearchProjectModel();

            searchProject.Projects =  _context.Projects.Where(p => p.Name.Contains(searchModel.Name)).ToList();


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
                var url = "https://gentsproject.s3.ca-central-1.amazonaws.com/" + searchProject.Documents.Where(p => p.DocumentId == doc.DocumentId).Single().Url.Replace(" ", "+");
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
            if (HomeController.current_role == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            FindCreateProjectModel model = new FindCreateProjectModel();
            model.Projects = new List<Project>();

            if (findCreateModel.CreateProject)
            {

                if (findCreateModel.CreateProjectName != null)
                {
                    // Check if Project name exists
                    if (_context.Projects.Where(p => p.Name == findCreateModel.CreateProjectName).Any())
                    {
                        model.Message = "Cannot Create Project, Already Existed";
                        model.CreateProject = true;
                        return View(model);
                    }
                    else
                    {
                        Project newProject = new Project()
                        {
                            Name = findCreateModel.CreateProjectName,
                            Success = "Yes"
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

        [HttpPost]
        public IActionResult Export(Export project)
        {

            var projects = _context.Projects.ToList();

            var documents = _context.Documents.ToList();

            var factors = _context.Factors.ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var proj in projects)
            {

                proj.Documents = documents.Where(d => d.ProjectFk == proj.ProjectId).ToList();
                proj.Factors = new List<Factor>();

                if (proj.Documents != null && proj.Documents.Count() > 0) {
                    foreach (var doc in proj.Documents)
                    {
                        IList<DocumentFactorRel> rels = _context.DocumentFactorRels.Where(d => d.DocumentFk == doc.DocumentId).ToList();

                        foreach (var rel in rels)
                        {
                            proj.Factors.Add(_context.Factors.Where(f => f.FactorId == rel.FactorFk).SingleOrDefault());
                        }
                    }
                }

                int[] factorsForFile = new int[88];
                factorsForFile[87] = 1; // last is success, zero indexed

                foreach (var fac in proj.Factors)
                {
                    factorsForFile[fac.Position - 1] = 1;
                }

                for (int i = 0; i <= 87; i++)
                {
                    sb.Append(factorsForFile[i].ToString());

                    if (i != 87)
                    {
                        sb.Append(",");
                    }
                }

                sb.Append("\n");
            }

            // Print file
            try
            {
                return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "AllProjects.csv");
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not create file", ex);
                return View(project);

            }
                
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
            model.FactorDescriptors = listFactors;



            return View(model);
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
                TempData["message"] = "Removed the project successfully!!";
                return RedirectToAction("SearchProjects", "Project");
            }
            else
            {
                TempData["message"] = "Please select a project";
                return RedirectToAction("SearchProjects", "Project");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteProj(ViewProjectModel model, int projectId)
        {
            int id = 0;

            if(model != null && model.SelectedProject != null && model.SelectedProject.ProjectId > 0)
            {
                id = model.SelectedProject.ProjectId;
            } else
            {
                id = projectId;
            }

            //FavorProj proj = _context.FavorProjs.Where(p => p.Url == url).Single();
            var url = "~/Project/ViewProject/" + id.ToString();
            if (_context.FavorProjs.Where(p => p.ProjectId == id).FirstOrDefault() == null)
            {
                var proj = new FavorProj() { ProjectId = id, Url = url };
                _context.FavorProjs.Add(proj);
                await _context.SaveChangesAsync();
                TempData["message"] = "Added favorite project successfully!!";
            }
            else
            {
                TempData["message"] = "There is no project added or the project was already your favorite!!";
            }
            return Redirect(url);
        }

        /*
         * AWS Credentials
         */
        string AWS_accessKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_accessKey"];
        string AWS_secretKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BucketSettings")["AWS_secretKey"];
        string AWS_bucketName = "gentsproject";
        [HttpPost]
        public async Task<IActionResult> DeleteProj(ViewProjectModel model, int projectId)
        {
            AmazonS3Client s3Client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, Amazon.RegionEndpoint.CACentral1);

            int id = 0;

            if (model != null && model.SelectedProject != null && model.SelectedProject.ProjectId > 0)
            {
                id = model.SelectedProject.ProjectId;
            }
            else
            {
                id = projectId;
            }

            Project project = new Project();
            project = await _context.Projects.FindAsync(id);
            project.Documents = await _context.Documents.Where(c => c.ProjectFk == id).ToListAsync();
            //TempData["message"] = "successfully!!"+project.Name;
            List<DocumentFactorRel> docFacRels = new List<DocumentFactorRel>();
            List<Document> docs = (List<Document>)project.Documents;
            foreach (var doc in docs)
            {
                docFacRels = await _context.DocumentFactorRels.Where(d => d.DocumentFk == doc.DocumentId).ToListAsync();
                if(docFacRels.FirstOrDefault()!=null)
                {
                    foreach (var docFacRel in docFacRels)
                    {
                        _context.DocumentFactorRels.Remove(docFacRel);
                        await _context.SaveChangesAsync();
                    }
                }
                //Document docRemove = _context.Documents.Find(doc.DocumentId);
            }

            foreach (var doc in docs.ToList())
            {
                if (doc != null)
                {
                    try
                    {
                        DeleteObjectRequest request = new DeleteObjectRequest
                        {
                            BucketName = AWS_bucketName,
                            Key = doc.Url
                        };
                        await s3Client.DeleteObjectAsync(request);
                    }catch(AmazonS3Exception ex)
                    {
                        TempData["message"] = ex.Message;
                    }
                    
                    _context.Documents.Remove(doc);
                    await _context.SaveChangesAsync();
                }
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            TempData["message"] = "Deleted the project successfully!!";
            return RedirectToAction("SearchProjects", "Project");
        }
    }
}