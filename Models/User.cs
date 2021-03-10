using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementCollection.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [ForeignKey("PermissionId")]
        public int PermissionLevel { get; set; }
        public Permission Permission { get; set; }
    }
}
