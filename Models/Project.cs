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

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Year Completed")]
        public DateTime DateCompleted { get; set; }

        [Required]
        [MaxLength(50)]
        public string Client { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        [Required]
        public bool Success { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }

        [NotMapped]
        public ICollection<Document> Documents { get; set; }

        [NotMapped]
        public ICollection<Factor> Factors { get; set; } 

    }
}
