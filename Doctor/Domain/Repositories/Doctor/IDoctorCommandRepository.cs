using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.Doctor
{
    public interface IDoctorCommandRepository: IBaseCommonCommandMethodRepository<Entities.EntityClass.Doctor>
    {
        Task<Response<bool>> UpdateActiveStatus(int doctorId, bool isActive);
        Task<Response<bool>> UpdateOnlineStatus(int doctorId, bool isOnline);
        Task<Response<bool>> UpdateExpertise(int doctorId, string expertise);
        Task<Response<bool>> UpdateProfileStep(int doctorId, int profileStep);
    }
}
