using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Prescription.Domain.Repositories.PrescriptionDiagonosis
{
    public interface IPrescriptionDiagonsisCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>
    {
    }
}
