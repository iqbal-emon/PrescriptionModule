using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.DoctorEntity
{
    public class DoctorSpecialization
    {
        [Key]
        public int DoctorSpecializationID { get; set; }

        [ForeignKey("Doctor")]
        [Required]
        public int DoctorID { get; set; }

        public int? SpecialityID { get; set; }

        public int? SpecializationID { get; set; }

        [MaxLength(500)]
        public string? ServiceDetails { get; set; }

        [MaxLength(200)]
        public string? DocumentName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}

