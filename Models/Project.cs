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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Uploaded Date")]
        public DateTime Uploaded { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Completed Date")]
        public DateTime DateCompleted { get; set; }

        [Required]
        [MaxLength(50)]
        public string Client { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        [Required]
        public string Success { get; set; }

        //tim
        public int Uploader_id { get; set; }

        public User User { get; set; }

        [ForeignKey("ProjectDocFk")]
        public ICollection<Document> Documents { get; set; }

        [NotMapped]
        public Dictionary<string, string> Factors { get; set; }

    }
}
