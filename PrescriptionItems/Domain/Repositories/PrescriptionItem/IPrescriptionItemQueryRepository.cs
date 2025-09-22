using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace PrescriptionItems.Domain.Repositories.PrescriptionItem
{
    public interface IPrescriptionItemQueryRepository: IBaseCommonQueryMethodRepository<Entities.EntityClass.PrescriptionEntity.PrescriptionItem>
    {
    }
}
