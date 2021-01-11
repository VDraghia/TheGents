using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class UserProject
    {
        public User User { get; set; }
        public Project Project { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}
