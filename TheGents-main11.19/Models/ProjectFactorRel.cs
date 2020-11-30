using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementCollection.Models
{
    public class ProjectFactorRel
    {
        public int ProjectFactorRelId { get; set; }

        [Required]
        [ForeignKey("ProjectId")]
        public int ProjectFk { get; set; }

        [Required]
        [ForeignKey("FactorId")]
        public int FactorFk { get; set; }
    }
}
