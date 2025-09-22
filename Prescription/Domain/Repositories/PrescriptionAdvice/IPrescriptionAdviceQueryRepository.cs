using Prescription.Dtos.ResponseDto.PrescriptionAdvice;
using Prescription.Dtos.ResponseDto.PrescriptionPatientHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Prescription.Domain.Repositories.PrescriptionAdvice
{
    public interface IPrescriptionAdviceQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>
    {
        Task<Response<List<PrescriptionAdviceResponseDto>>> GetByPrescriptionId(int Id);



    }
}
