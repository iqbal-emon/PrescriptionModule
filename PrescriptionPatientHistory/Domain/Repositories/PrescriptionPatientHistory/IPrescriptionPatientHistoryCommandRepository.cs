using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace PrescriptionPatientHistory.Domain.Repositories.PrescriptionPatientHistory
{
    public interface IPrescriptionPatientHistoryCommandRepository: IBaseCommonCommandMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>
    {
    }
}
