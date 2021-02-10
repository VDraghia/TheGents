using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
