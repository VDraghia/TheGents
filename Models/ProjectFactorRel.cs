using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementCollection.Models
{
    public class ProjectFactorRel
    {
        public int ProjectFactorRelId { get; set; }

        [Required]
        [ForeignKey("ProjectId")]
        public int ProjectFk { get; set; }

        [Required]
        [ForeignKey("FactorId")]
        public int FactorFk { get; set; }
    }
}
