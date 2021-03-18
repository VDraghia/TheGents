using ProjectManagementCollection.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models.ViewModels
{
    public class SearchProjectModel
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string UploaderEmail { get; set; }

        public DateTime? DateRangeMin { get; set; }

        public DateTime? DateRangeMax { get; set; }

        [MaxLength(50)]
        public string Client { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        public IList<Project> Projects { get; set; }

        public IEnumerable<Project> AllProjects { get; set; }
        public IEnumerable<Document> Documents { get; set; }
        public IEnumerable<FavorProj> FavorProjs { get; set; }
        public IEnumerable<FavorDoc> FavorDocs { get; set; }

    }
}
