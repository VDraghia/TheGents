using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}
