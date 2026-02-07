using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Notification.Domain.Repositories.Notification
{
    public interface INotificationCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.Notification>
    {
        
    }
}

