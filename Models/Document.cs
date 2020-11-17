using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models
{
    public class Document
    {
        public int DocumentId { get; set;}

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Url { get; set; }

        [ForeignKey("ProjectId")]
        public int ProjectFk { get; set; }
        public Project Project { get; set; }

    }
}
