using Entities.EntityClass;
using Medication.Dtos.ResponseDto.MedicationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Medication.Domain.Repositories.Medication
{
    public interface IMedicationQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.MedicineEntity.Medication>
    {
        Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetMedicationByName(string name,string uniCode);
        Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetBookMarks(int doctorId);
        Task<PagedWithResponse<List<MedicationMostUsedDto>>> GetAllMedicineMostUsed(
         int pageNumber,
         int pageSize,
         string? searchTerm = null,
         string? manufacturerName = null,
         string? days=null);



    }
}
