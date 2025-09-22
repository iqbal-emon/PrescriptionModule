using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CountryEntity
{
    public class Location
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public int CityID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
