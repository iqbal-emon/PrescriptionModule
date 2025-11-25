using Appointment.Dtos.ResponseDto.AppointmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Appointment.Domain.Repositories.Appointment
{
    public interface  IAppointmentQueryRepository:IBaseCommonQueryMethodRepository<Entities.EntityClass.Appointment>
    {
        public Task<Response<List<AppointmentApiResponseDto>>> GetAll(int doctorId);


    }
}
