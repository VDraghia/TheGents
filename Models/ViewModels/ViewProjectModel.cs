using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class ViewProjectModel
    {

        [MaxLength(50)]
        public string Name { get; set; }

        public IList<Factor> MatchingFactors { get; set; }

        IList<Project> MatchingProjects { get; set; }
    }
}
