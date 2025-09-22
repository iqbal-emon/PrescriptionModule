using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.DoctorEntity
{
    public class DoctorSchedule
    {
        public int DoctorScheduleID { get; set; }
        public int DoctorID { get; set; }
        public int ScheduleID { get; set; }
        public int TenantID { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public DateTime? UpdatedAt { get; set; }= DateTime.Now;

        public bool IsDeleted { get; set; }

    }
}
