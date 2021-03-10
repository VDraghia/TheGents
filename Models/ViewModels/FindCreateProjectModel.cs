using System.Collections.Generic;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class FindCreateProjectModel
    {
        public string FindProjectName { get; set; }

        public string CreateProjectName { get; set; }

        public bool CreateProject { get; set; }

        public bool FindProject { get; set; }

        public bool CreateSuccess { get; set; }

        public string Message { get; set; }
        public IList<Project> Projects { get; set; }
    }
}
