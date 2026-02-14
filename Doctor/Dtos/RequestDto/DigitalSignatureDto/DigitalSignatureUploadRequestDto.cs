using Microsoft.AspNetCore.Http;

namespace Doctor.Dtos.RequestDto.DigitalSignatureDto
{
    public class DigitalSignatureUploadRequestDto
    {
        public IFormFile? File { get; set; }
        public int DoctorID { get; set; }
        public int TenantID { get; set; }
        public int? CreatedBy { get; set; }
    }
}

