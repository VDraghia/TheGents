using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManagementCollection.Data;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementCollection.Controllers
{
    public class ViewDocumentController : Controller
    {

        private readonly ILogger<ViewDocumentController> _logger;
        private readonly PmcAppDbContext _context;
        public ViewDocumentController(PmcAppDbContext context, ILogger<ViewDocumentController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [Route("~/ViewDocument/{id}")]
        public IActionResult ViewDocument(int id)
        {
            //Get the project by id
            Project project = _context.Projects.Where(c => c.ProjectId == id).Single();

            //Get the project factor relationships
            List<DocumentFactorRel> projFactors = _context.DocumentFactorRels.Where(c => c.DocumentFk == id).ToList();


            List<Factor> factors = new List<Factor>();

            // Get the Factors related to the Projects
            foreach (DocumentFactorRel projFac in projFactors)
            {
                factors.Add(_context.Factors.Single(c => c.FactorId == projFac.FactorFk));
            }

            Dictionary<string, string> factorDescriptions = new Dictionary<string, string>();

            //Get Main and Sub Categories for description
            foreach (Factor factor in factors)
            {
                FactorMainCategory value = _context.FactorMainCategories.Single(c => c.FactorMainCategoryId == factor.FactorMainCategoryFk);
                FactorSubCategory key = _context.FactorSubCategories.Single(c => c.FactorSubCategoryId == factor.FactorSubCategoryFk);

                factorDescriptions.Add(key.FactorSubCategoryDesc, value.FactorMainCategoryDesc);

            }

            /*
            List<Document> documents = new List<Document>();
            if(project.Name == "Project1")
                documents.Add(new Document { DocumentId = 1, Name = "Project1", Url = @"/pdf/project1.pdf", ProjectFk = 1 });
            if (project.Name == "Project2")
                documents.Add(new Document { DocumentId = 2, Name = "Project2", Url = @"/pdf/project2.pdf", ProjectFk = 1 });
            if (project.Name == "Project3")
                documents.Add(new Document { DocumentId = 3, Name = "Project3", Url = @"/pdf/project3.pdf", ProjectFk = 2 });

            project.Documents = documents; 

            */

            return View(project);
        }
    }
}
