using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.DoctorEntity
{
    public class ExpertiseCategory
    {
        public int ExpertiseID { get; set; }
        public int TenantID { get; set; }
        public string ExpertiseName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
