using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class Document
    {
        public int DocumentId { get; set;}

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Url { get; set; }

        public int ProjectDocFk { get; set; }

        public Project Project { get; set; }

    }
}
