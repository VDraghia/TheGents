using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models
{
    public class Factor
    {
        public int FactorId { get; set; }

        [Required]
        [ForeignKey("FactorMainCategoryId")]
        public int FactorMainCategoryFk { get; set; }

        [Required]
        [ForeignKey("FactorSubCategoryId")]
        public int FactorSubCategoryFk { get; set; }

        [MaxLength(100)]
        public string FactorDesc { get; set; }

    }
}
