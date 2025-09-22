using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.InstituteEntity
{
    public class InstituteLocation
    {
        public int LocationID { get; set; }
        public int InstituteID { get; set; }
        public string? State { get; set; }
        public int? StateID { get; set; }
        public string Country { get; set; } = string.Empty;
        public int? CountryID { get; set; }
        public string City { get; set; } = string.Empty;
        public int? CityID { get; set; }
        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
