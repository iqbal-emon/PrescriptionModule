using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PatientEntity
{
    public class PrescriptionDisease : BaseCommonEntityField
    {
        public int PrescriptionDiseaseID { get; set; }
        public int DiseaseID { get; set; }
        public int PrescriptionID { get; set; }
    }
}
