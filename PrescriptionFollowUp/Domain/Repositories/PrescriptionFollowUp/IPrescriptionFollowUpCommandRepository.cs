using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace PrescriptionFollowUp.Domain.Repositories.PrescriptionFollowUp
{
    public interface IPrescriptionFollowUpCommandRepository: IBaseCommonCommandMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>
    {
    }
}
