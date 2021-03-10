using System;
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

        public IList<Factor> MustHaveFactors { get; set; }

        public IList<Factor> NotHaveFactors { get; set; }
 
    }
}
