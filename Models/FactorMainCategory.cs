using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models
{
    public class FactorMainCategory
    {
        public int MainCategoryId { get; set; }

        [Required]
        public string MainCategory { get; set; }

    }
}
