using Entities.EntityClass;
using PrescriptionPdf.Dtos.ResponseDto.PrescriptionPdfDto;
using Utility.BaseInterface;
using Utility.Response;

namespace PrescriptionPdf.Domain.Repositories.PrescriptionPdf
{
    public interface IPrescriptionPdfQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>
    {
        Task<Response<List<PrescriptionPdfPatientResponseDto>>> GetByPatientDoctorId(int patientId,int doctorId);
        Task<Response<List<PrescriptionPdfPatientResponseDto>>> GetPrehandByDoctorId(int? doctorId,string? prescriptionCode,string? patientName,string? patientCode);
        Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionPdf>> GetByAppointmentId(int appointmentId);

        

    }
}
