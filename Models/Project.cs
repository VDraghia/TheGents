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
        public string Name { get; set; }

        [Required]
        public DateTime Uploaded{ get; set;}

        [Required]
        public DateTime DateCompleted { get; set; }

        [Required]
        public string Client { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public Boolean Success { get; set; }

        [ForeignKey("Document")]
        public ICollection<Document> Documents { get; set; }

        [ForeignKey("Factor")]
        public ICollection<Factor> Factors { get; set; }


    }
}
