using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class ProjectViewModel
    {

        [MaxLength(50)]
        public string Location { get; set; }

        [MaxLength(50)]
        public string Client { get; set; }

        [RegularExpression(@"[1-9][0-9][0-9][0-9]$")]
        public string DateCompleted { get; set; }

        public string Error { get; set; }

        List<Project> MatchingProjects { get; set; }
    }
}
