using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PrescriptionEntity
{
    public class PrescriptionAdvice : BaseCommonEntityField
    {
        [Key]
        public int PrescriptionAdviceID { get; set; }

        [ForeignKey("Prescription")]
        [Required]
        public int PrescriptionID { get; set; }
        [ForeignKey("CommonAdvice")]
        [Required]
        public int CommonAdviceID { get; set; }
  

    }
}
