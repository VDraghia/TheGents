using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class FactorSubCategory
    {

        public int FactorSubCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FactorSubCategoryDesc { get; set; }

    }
}
