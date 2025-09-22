using Prescription.Dtos.ResponseDto.PrescriptionPatientHistory;
using Prescription.Dtos.ResponseDto.PrescriptionSymtomDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Prescription.Domain.Repositories.PrescriptionSymtom
{
    public interface IPrescriptionSymtomQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>
    {
        Task<Response<List<PrescriptionSymtomResponseDto>>> GetByPrescriptionId(int Id);
    }
}
