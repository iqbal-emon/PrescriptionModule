using Prescription.Dtos.ResponseDto.PrescriptionDiagonsisDto;
using Prescription.Dtos.ResponseDto.PrescriptionItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Prescription.Domain.Repositories.PrescriptionItem
{
    public interface IPrescriptionItemQueryRepository: IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionItem>
    {
        Task<Response<List<PrescriptionItemResponseDto>>> GetByPrescriptionId(int Id);

    }
}
