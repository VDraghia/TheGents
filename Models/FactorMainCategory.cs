using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class FactorMainCategory
    {

        public int FactorMainCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FactorMainCategoryDesc { get; set; }

    }
}
