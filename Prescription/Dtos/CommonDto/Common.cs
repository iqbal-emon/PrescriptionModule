using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.CommonDto
{
    public class Common
    {
        public int? PatientId { get; set; }
        public int? userId { get; set;}
        public int? TenantId { get; set; }
        public int? DoctorId { get; set; }
    }
}
