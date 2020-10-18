using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace ProjectManagementCollection.Models
{
    public class UploadModel
    {
        [Required]
        public string DocumentName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string UploadDate { get; set; }

        [Required]
        public string Client { get; set; }

        [Required]
        public bool Success { get; set; }

        [Required]
        public List<Boolean> Factors { get; set; }



    }
}

