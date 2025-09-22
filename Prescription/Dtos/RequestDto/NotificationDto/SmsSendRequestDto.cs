using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.NotificationDto
{
    public class SmsSendRequestDto
    {
        public string? MobileNo { get; set; }
        public string? Sms { get; set; }
        public string? ApiKey { get; set; }
    }
}
