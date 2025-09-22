using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PrescriptionEntity
{
    public class PrescriptionFollowUp : BaseCommonEntityField
    {
        public int PrescriptionFollowUpID { get; set; }
        public int FollowUpID { get; set; }
        public int PrescriptionID { get; set; }
      
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}
