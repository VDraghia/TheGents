using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementCollection.Models
{
    public class FavorProj
    {
        public int FavorProjId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Url { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }



    }
}
