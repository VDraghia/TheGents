using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class FactorCate
    {
        public IEnumerable<FactorSubCategory> FactorSubCategories { get; set; }

        [BindProperty]
        public List<int> AreChecked { get; set; }
        public Dictionary<string, int> Factors { get; set; }

    }
}
