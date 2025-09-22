using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace ScannedPrescription.Domain.Repositories.ScannedPrescription
{
    public interface IScannedPrescrptionCommandRepository: IBaseCommonCommandMethodRepository<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>
    {
    }
}
