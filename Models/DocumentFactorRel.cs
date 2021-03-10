using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementCollection.Models
{
    public class DocumentFactorRel
    {
        public int DocumentFactorRelId { get; set; }

        [Required]
        [ForeignKey("DocumentId")]
        public int DocumentFk { get; set; }

        public Document Document { get; set; }

        [Required]
        [ForeignKey("FactorId")]
        public int FactorFk { get; set; }

        public Factor Factor { get; set; }
    }
}
