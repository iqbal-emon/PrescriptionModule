using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Prescription.Domain.Repositories.PrescriptionExamination
{
    public interface IPrescriptionExminationCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionExamination>
    {
    }
}
