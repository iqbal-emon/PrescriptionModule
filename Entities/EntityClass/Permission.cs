using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Permission
    {
        public int PermissionId { get; set; }

        public string PolicyName { get; set; } = string.Empty;

        public string PermissionKey { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
