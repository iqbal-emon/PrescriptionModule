using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.CampaignDoctor
{
    public interface ICampaignDoctorCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DoctorEntity.CampaignDoctor>
    {
    }
}

