using Prescription.Dtos.ResponseDto.PrescriptionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;
using static Dapper.SqlMapper;


namespace Prescription.Domain.Repositories.Prescription
{
    public interface IPrescriptionQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.Prescription>
    {
        public Task<Response<PrescriptionAnalyticsDto>> GetAnalytics();

    }
}
