using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DigitalSignature
{
    public interface IDigitalSignatureQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.DigitalSignature>
    {
        Task<Response<Entities.EntityClass.DoctorEntity.DigitalSignature>> GetByDoctorId(int doctorId);
    }
}

