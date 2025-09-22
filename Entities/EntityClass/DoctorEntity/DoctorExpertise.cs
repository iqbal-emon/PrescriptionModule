using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.DoctorEntity
{
    public class DoctorExpertise
    {
        public int DoctorExpertiseID { get; set; }
        public int TenantID { get; set; }
        public int DoctorID { get; set; }
        public int ExpertiseID { get; set; }
        public int? ExperienceYears { get; set; }
        public string? Certification { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
