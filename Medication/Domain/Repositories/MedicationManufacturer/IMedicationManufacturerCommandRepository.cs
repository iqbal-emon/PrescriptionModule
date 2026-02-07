using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Medication.Domain.Repositories.MedicationManufacturer
{
    public interface IMedicationManufacturerCommandRepository: IBaseCommonCommandMethodRepository<Entities.EntityClass.MedicineEntity.MedicationManufacturer>
    {
    }
}
