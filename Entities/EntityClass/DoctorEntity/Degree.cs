using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.DoctorEntity
{
    public class Degree
    {
        public int DegreeID { get; set; }
        public int TenantID { get; set; }
        public string DegreeName { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string? DurationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
