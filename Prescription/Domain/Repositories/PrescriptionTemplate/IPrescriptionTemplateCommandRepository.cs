using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Prescription.Domain.Repositories.PrescriptionTemplate
{
    public interface IPrescriptionTemplateCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate>
    {
    }
}
