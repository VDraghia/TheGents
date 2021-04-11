using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementCollection.Models
{
    public class Export
    {
        public int ProjectDocFk { get; set; }

        public IEnumerable<Project> Projects { get; set; }

        [Required]
        public List<Boolean> Factors { get; set; }
    }
}
