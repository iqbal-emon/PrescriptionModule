using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace PrescriptionInvestigation.Domain.Repositories.PrescriptionInvestigation
{
    public interface IPrescriptionInvestigationCommandRepository: IBaseCommonCommandMethodRepository<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>
    {
    }
}
