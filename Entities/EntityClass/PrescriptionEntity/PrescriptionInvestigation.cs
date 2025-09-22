using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PatientEntity
{
    public class PrescriptionInvestigation : BaseCommonEntityField
    {
        public int PrescriptionInvestigationID { get; set; }
        public int InvestigationID { get; set; }
        public int PrescriptionID { get; set; }
    }
}
