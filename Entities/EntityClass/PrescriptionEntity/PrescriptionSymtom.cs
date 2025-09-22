using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PrescriptionEntity
{
    public class PrescriptionSymtom : BaseCommonEntityField
    {
        public int PrescriptionSymtomID { get; set; }
        public int SymtomID { get; set; }
        public int PrescriptionID { get; set; }
        public string? PastSymtom { get; set; }
        public string? PresentSymtom { get; set; }

        public string days { get; set; }

        public string duration { get; set; }


    }
}
