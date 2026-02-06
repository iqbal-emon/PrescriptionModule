using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor.Dtos.ResponseDto.DoctorChamberDto
{
    public class DistrictApiResponseDto
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int divisionID { get; set; }
    }
}

