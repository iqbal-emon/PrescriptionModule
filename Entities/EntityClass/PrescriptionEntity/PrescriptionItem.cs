using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Entities.EntityClass.MedicineEntity;

namespace Entities.EntityClass.PrescriptionEntity
{
    public class PrescriptionItem
    {
        [Key]
        public int PrescriptionItemId { get; set; }

        [Required]
        public int PrescriptionId { get; set; }


        [Required]
        public int MedicationId { get; set; }

        [MaxLength(50)]
        public string Dosage { get; set; } // Example: 1 tablet, 2 times a day

        public int Quantity { get; set; }

        [MaxLength(255)]
        public string Instructions { get; set; } // Example: Take with food
        public string Duration { get; set; }
        public string MealTime { get; set; }
        public string Timing {  get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }
}
