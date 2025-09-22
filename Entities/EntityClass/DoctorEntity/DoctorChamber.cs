using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.DoctorEntity
{
    public class DoctorChamber
    {
        public int ChamberID { get; set; }
        public int TenantID { get; set; }
        public int DoctorID { get; set; }
        public string ChamberName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int? CountryID { get; set; }
        public string City { get; set; } = string.Empty;
        public int? CityID { get; set; }
        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }
        public bool IsVisibleOnPrescription { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
