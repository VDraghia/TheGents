using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class UserProjectViewModel
    {
        public User User { get; set; }
        public Project Project { get; set; }
        public IEnumerable<Project> Projects { get; set; }

        public IEnumerable<FactorSubCategory> FactorSubCategories { get; set; }

        [BindProperty]
        public List<int> AreChecked { get; set; }
        public Dictionary<int, Tuple<string, string>> Factors { get; set; }
    }
}
