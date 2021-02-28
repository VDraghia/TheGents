using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementCollection.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        [ForeignKey("Uploader_id")]
        [Required]
        public int Permission { get; set; }

        [ForeignKey("ProjectId")]
        public ICollection<Project> Projects { get; set; }

    }
}
