using Entities.EntityClass.PatientEntity;
using Utility.BaseInterface;

namespace PatienFolowUp.Domain.Repositories.Patients
{
    public interface IPatientsCommandRepository : IBaseCommonCommandMethodRepository<Patient>
    {
    }
}
