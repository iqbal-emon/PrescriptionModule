using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.InstituteEntity
{
    public class Institute
    {
        public int InstituteId { get; set; }
        public string InstituteName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? Website { get; set; }
        public int? EstablishedYear { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
