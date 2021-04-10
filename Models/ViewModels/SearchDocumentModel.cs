using System.Collections.Generic;

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

        public IList<Factor> NotHaveFactors { get; set; }

    }
}