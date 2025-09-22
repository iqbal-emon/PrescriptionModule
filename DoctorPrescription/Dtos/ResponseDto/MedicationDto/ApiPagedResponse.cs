using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;

namespace Medication.Dtos.ResponseDto.MedicationDto
{
    public class ApiPagedResponse<T> : ApiResponse<T>
    {
        public int TotalCount { get; set; }
    }

}
