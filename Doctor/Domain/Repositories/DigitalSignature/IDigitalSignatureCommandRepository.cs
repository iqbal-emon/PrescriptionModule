using System;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DigitalSignature
{
    public interface IDigitalSignatureCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DoctorEntity.DigitalSignature>
    {
    }
}

