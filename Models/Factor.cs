using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models
{
    public class Factor
    {
        public int FactorId { get; set; }

        [Required]
        public int MainCategoryFk { get; set; }

        [Required]
        public int SubCategoryFk { get; set; }

    }
}
