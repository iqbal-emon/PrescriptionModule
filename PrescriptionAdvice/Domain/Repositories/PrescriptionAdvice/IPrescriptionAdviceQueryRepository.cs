using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace DoctorPrescription.Domain.Repositories.PrescriptionAdvice
{
    public interface IPrescriptionAdviceQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>
    {
    }
}
