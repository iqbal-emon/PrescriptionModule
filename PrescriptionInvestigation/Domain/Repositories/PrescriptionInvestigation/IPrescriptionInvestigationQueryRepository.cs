using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace PrescriptionInvestigation.Domain.Repositories.PrescriptionInvestigation
{
    public interface IPrescriptionInvestigationQueryRepository: IBaseCommonQueryMethodRepository<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>
    {
    }
}
