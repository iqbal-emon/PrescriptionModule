using Microsoft.AspNetCore.Http;
using Prescription.Dtos.RequestDto.PrescriptionCreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionDto
{
    public class PrescriptionCreateDto
    {
        public PrescriptionRequestDto? InsertRequest { get; set; }
        public IFormFile? uploadImage { get; set; }
    }
}
