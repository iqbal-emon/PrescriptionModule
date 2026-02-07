using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Notification.Domain.Repositories.Notification
{
    public interface INotificationQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.Notification>
    {
        Task<Response<List<Entities.EntityClass.Notification>>> GetByUserId(int userId, string role = "");
    }
}

