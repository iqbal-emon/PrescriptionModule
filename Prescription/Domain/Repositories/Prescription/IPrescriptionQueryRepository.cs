using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;


namespace Prescription.Domain.Repositories.Prescription
{
    public interface IPrescriptionQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.Prescription>
    {
    }
}
