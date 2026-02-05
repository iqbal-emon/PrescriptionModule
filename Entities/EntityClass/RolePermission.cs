using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class RolePermission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Required]
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }

        // Navigation Properties
        public Role? Role { get; set; }
        public Permission? Permission { get; set; }
    }
}
