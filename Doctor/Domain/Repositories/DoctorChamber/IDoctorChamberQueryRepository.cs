using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DoctorChamber
{
    public interface IDoctorChamberQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.DoctorChamber>
    {
        Task<ApiResponse<List<Entities.CountryEntity.District>>> GetAllDistrict(int divisonId);
        Task<ApiResponse<List<Entities.CountryEntity.Division>>> GetAllDivision();
        Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>> GetByDoctorId(int doctorId);
    }
}

