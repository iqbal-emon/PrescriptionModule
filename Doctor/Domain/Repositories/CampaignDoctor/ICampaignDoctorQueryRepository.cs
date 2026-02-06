using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.CampaignDoctor
{
    public interface ICampaignDoctorQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.CampaignDoctor>
    {
        Task<Response<List<Entities.EntityClass.DoctorEntity.CampaignDoctor>>> GetByDoctorId(int doctorId);
    }
}

