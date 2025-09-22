using Prescription.Dtos.ResponseDto.PrescriptionInvestigationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Prescription.Domain.Repositories.PrescriptionInvestigation
{
    public interface IPrescriptionInvestigationQueryRepository: IBaseCommonQueryMethodRepository<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>
    {
        Task<Response<List<PrescriptionInvestigationResponseDto>>> GetByPrescriptionId(int Id);
    }
}
