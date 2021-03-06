using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class FactorDocuments
    {
        public ICollection<Document> Documents { get; set; }
        [BindProperty]
        public List<int> factorsHas { get; set; }
        [BindProperty]
        public List<int> factorsNotHas { get; set; }
        public Dictionary<int, Tuple<string, string>> Factors { get; set; }
    }
}
