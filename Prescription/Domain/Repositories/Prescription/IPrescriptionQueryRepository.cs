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
        Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetByPatientId(int patientId);
        Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetByDoctorId(int doctorId);
        Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetByDoctorIdAndPatientId(int doctorId, int patientId);
        Task<Response<List<Entities.EntityClass.PrescriptionEntity.Prescription>>> GetByAppointmentCreatorId(int patientId);
        Task<Response<List<object>>> GetPatientDiseaseList(int patientId);
    }
}
