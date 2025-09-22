using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PrescriptionEntity
{
    public class PrescriptionTemplate : BaseCommonEntityField
    {
        public int PrescriptionTemplateID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PrescriptionID { get; set; }
        public int DoctorID { get; set; }
    }
}
