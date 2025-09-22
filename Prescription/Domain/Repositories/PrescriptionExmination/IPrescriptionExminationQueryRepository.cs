using Prescription.Dtos.ResponseDto.PrescriptionExaminationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Prescription.Domain.Repositories.PrescriptionExamination
{
    public interface IPrescriptionExminationQueryRepository: IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>
    {
        Task<Response<List<PrescriptionExaminationResponseDto>>> GetByPrescriptionId(int id);
    }
}
