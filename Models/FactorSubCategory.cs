using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models
{
    public class FactorSubCategory
    {
        public int SubCategoryId { get; set; }

        [Required]
        public string SubCategory { get; set; }
    }
}
