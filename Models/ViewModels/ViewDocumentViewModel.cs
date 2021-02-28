using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class ViewDocumentViewModel
    {
        [Required]
        public int UserId { get; set; }

        public bool RememberMe { get; set; }

    }
}
