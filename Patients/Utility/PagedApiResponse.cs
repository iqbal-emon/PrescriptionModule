using Patients.Dtos.ResponseDto.PatientsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;

namespace Patients.Utility
{
    public class PagedApiResponse<T> : ApiResponse<T>
    {
        internal List<PatientDataDto> Result;

        public int TotalCount { get; set; }
      
    }
}
