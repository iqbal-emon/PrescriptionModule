using Appointment.Dtos.ResponseDto.AppointmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;
using Appointment.Dtos.RequestDto.AppointmentDto;

namespace Appointment.Domain.Repositories.Appointment
{
    public interface  IAppointmentQueryRepository:IBaseCommonQueryMethodRepository<Entities.EntityClass.Appointment>
    {
        public Task<Response<List<AppointmentApiResponseDto>>> GetAll(int doctorId);
        public Task<Response<PagedWithResponse<List<AppointmentApiResponseDto>>>> GetAll(
                       int doctorId,
    int pageNumber = 1,
    int pageSize = 10,
    string search = null,
    int? sessionId = null,
    int? scheduleId = null);

        Task<Response<AppointmentResponseDto>> GetByAppointmentId(int id);


    }
}
