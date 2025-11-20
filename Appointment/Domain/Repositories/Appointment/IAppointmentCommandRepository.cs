using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Appointment.Domain.Repositories.Appointment
{
   public interface IAppointmentCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.Appointment>
    {

    }
   
}
