using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
