using Appointment.Dtos.RequestDto.AppointmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Appointment.Domain.Repositories.Appointment
{
   public interface IAppointmentCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.Appointment>
    {
        Task<Response<int>> Insert(AppointmentInsertRequestDto appointmentDto);
    }
   
}
