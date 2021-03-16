using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementCollection.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        public string Success { get; set; }

        [NotMapped]
        public ICollection<Document> Documents { get; set; }

        [NotMapped]
        public ICollection<Factor> Factors { get; set; }

        [NotMapped]
        public Dictionary<string, string> FactorStrings { get; set; }
    }
}
