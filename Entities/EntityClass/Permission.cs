using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(500)]
        public string PermissionValue { get; set; }
    }
}
