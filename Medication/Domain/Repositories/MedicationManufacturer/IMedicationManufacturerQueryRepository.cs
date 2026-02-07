using Medication.Dtos.ReponseDto.MedicationManufacturerDto;
using Medication.Dtos.ResponseDto.MedicationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;
using static Dapper.SqlMapper;

namespace Medication.Domain.Repositories.MedicationManufacturer
{
    public interface IMedicationManufacturerQueryRepository: IBaseCommonQueryMethodRepository<Entities.EntityClass.MedicineEntity.Medication>
    {
        Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetAll(string? manufacturerName);
        Task<PagedWithResponse<List<MedicationManufacturerApiReponseDto>>> GetAllManufacturerMostUsed(
       int pageNumber,
       int pageSize,
       string? searchTerm = null,
       string? manufacturerName = null,
       string? days = null);
    }
}
