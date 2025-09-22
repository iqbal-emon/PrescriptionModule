using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.DoctorEntity
{
    public class DoctorDegree
    {
        public int DoctorDegreeID { get; set; }
        public int TenantID { get; set; }
        public int DoctorID { get; set; }
        public int DegreeID { get; set; }
        public int PassingYear { get; set; }
        public string InstituteName { get; set; } = string.Empty;
        public int? InstituteID { get; set; }
        public string Country { get; set; } = string.Empty;
        public int? CountryID { get; set; }
        public string City { get; set; } = string.Empty;
        public int? CityID { get; set; }
        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
