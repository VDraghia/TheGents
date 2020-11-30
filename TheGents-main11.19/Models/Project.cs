using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime Uploaded { get; set; }

        [Required]
        public DateTime DateCompleted { get; set; }

        [Required]
        [MaxLength(50)]
        public string Client { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        [Required]
        public string Success { get; set; }

        [ForeignKey("ProjectDocFk")]
        public ICollection<Document> Documents { get; set; }

        [NotMapped]
        public Dictionary<string, string> Factors { get; set; }
       
    }
}
