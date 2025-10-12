using Entities.EntityClass.PatientEntity;
using PatienFolowUp.Dtos.ResponseDto.Patients;
using Patients.Dtos.ResponseDto.PatientsDto;
using Utility.BaseInterface;
using Utility.Response;
using static Dapper.SqlMapper;

namespace PatienFolowUp.Domain.Repositories.Patients
{
    public interface IPatientsQueryRepository : IBaseCommonQueryMethodRepository<Patient>
    {
        Task<Response<Entities.EntityClass.PatientEntity.Patient>> GetByRoleAndReferenceId(int referenceId);
        Task<Response<List<PatientAgeDistributionResponseDto>>> GetAgeDistribution();



        Task<PagedWithResponse<List<PatientDataDto>>> GetAll(int pageNumber = 1, int pageSize = 10, string searchTerm = "", int? doctorId = null);
    }
}
