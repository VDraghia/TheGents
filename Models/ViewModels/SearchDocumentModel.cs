using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class SearchDocumentModel
    {
        public int DocumentId { get; set; }

        public string DocumentName { get; set; }

        public Dictionary<int, string> ProjectNames { get; set; }

        public IList<Document> Documents { get; set; }

        public IList<ListFactorDescriptorModel> ListFactorDesc { get; set; }

        [BindProperty]
        public IList<int> MustHaveFactors { get; set; }
        [BindProperty]
        public IList<int> NotHaveFactors { get; set; }
 
    }
}
