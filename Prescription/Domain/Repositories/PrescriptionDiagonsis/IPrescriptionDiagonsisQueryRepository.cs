using Prescription.Dtos.ResponseDto.PrescriptionDiagonsisDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Prescription.Domain.Repositories.PrescriptionDiagonosis
{
    public interface IPrescriptionDiagonsisQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>
    {
        
        Task<Response<List<PrescriptionDiagonsisResponseDto>>> GetByPrescriptionId(int  Id);

    }
}
