using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProjectManagementCollection.Models.DescriptorModels;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class SearchDocumentModel
    {
        public int DocumentId { get; set; }

        public string DocumentName { get; set; }

        public Dictionary<int, string> ProjectNames { get; set; }

        public IList<Document> Documents { get; set; }

        public IList<ListFactorDescriptor> ListFactorDesc { get; set; }

        [BindProperty]
        public IList<int> MustHaveFactors { get; set; }
        [BindProperty]
        public IList<int> NotHaveFactors { get; set; }

    }
}