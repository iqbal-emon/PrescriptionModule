using Entities.EntityClass.PatientEntity;
using Utility.BaseInterface;
using Utility.Response;

namespace PatienFolowUp.Domain.Repositories.Patients
{
    public interface IPatientsQueryRepository : IBaseCommonQueryMethodRepository<Patient>
    {
        Task<Response<Entities.EntityClass.PatientEntity.Patient>> GetByRoleAndReferenceId(int referenceId);
    }
}
