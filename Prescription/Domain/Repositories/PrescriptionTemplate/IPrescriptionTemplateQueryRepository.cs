using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Prescription.Domain.Repositories.PrescriptionTemplate
{
    public interface IPrescriptionTemplateQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>
    {
        Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>>> GetPrescriptionTemplatesByDoctorId(int doctorId,string name);
    }
}
