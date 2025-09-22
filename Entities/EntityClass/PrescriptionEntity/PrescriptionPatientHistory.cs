using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PrescriptionEntity
{
    public class PrescriptionPatientHistory : BaseCommonEntityField
    {
        public int PrescriptionPatientHistoryID { get; set; }
        public int CommonHistoryID { get; set; }
        public int PrescriptionID { get; set; }
        public string? PastHistory { get; set; }
        public string? PresentHistory { get; set; }
    }
}
