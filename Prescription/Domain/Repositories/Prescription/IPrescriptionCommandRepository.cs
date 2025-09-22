using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;



namespace Prescription.Domain.Repositories.Prescription
{
    public interface  IPrescriptionCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.PrescriptionEntity.Prescription>
    {
    }
}
