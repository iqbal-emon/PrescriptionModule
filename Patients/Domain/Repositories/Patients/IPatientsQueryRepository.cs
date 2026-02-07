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

        Task<Response<PatientsApiResponseDto>> GetById(int Id);
        Task<Response<PatientsApiResponseDto>> GetByPhoneNo(string phoneNo);
        Task<Response<PatientsApiResponseDto>> GetByPhoneAndCode(string pCode, string pPhone);
        Task<Response<PatientsApiResponseDto>> GetByUserName(string userName);
        Task<Response<PatientsApiResponseDto>> GetByUserId(int userId);
        Task<Response<List<PatientsApiResponseDto>>> GetAllPatients();
        Task<Response<List<PatientsApiResponseDto>>> GetPatientListByUserProfileId(int profileId, string role);
        Task<Response<List<PatientsApiResponseDto>>> GetPatientListBySearchUserProfileId(int profileId, string role, string name);
        Task<Response<List<PatientsApiResponseDto>>> GetPatientListFilter(string searchTerm = "");
        Task<Response<List<PatientsApiResponseDto>>> GetPatientListByAgentMaster(int masterId);
        Task<Response<List<PatientsApiResponseDto>>> GetPatientListByAgentSupervisor(int supervisorId);


        Task<PagedWithResponse<List<PatientDataDto>>> GetAll(int pageNumber = 1, int pageSize = 10, string searchTerm = "", int? doctorId = null,string followupdate = "");
        // Repository থেকে শুধু raw data return করবে (PatientDataDto)
        Task<PagedWithResponse<List<PatientDataDto>>> GetFollowUpPatients(int? doctorId, string startDate, string endDate);
    }
}
