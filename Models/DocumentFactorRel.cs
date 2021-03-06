using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementCollection.Models
{
    public class DocumentFactorRel
    {
        public int DocumentFactorRelId { get; set; }

        [Required]
        [ForeignKey("DocumentId")]
        public int DocumentId { get; set; }

        public Document document { get; set; }
        [Required]
        [ForeignKey("FactorId")]
        public int FactorId { get; set; }
        public Factor factor { get; set; }
    }
}
