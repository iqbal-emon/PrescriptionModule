using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Pharmacies.Domain.Repositories.Pharmacies
{
    public interface IPharmaciesCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.Pharmacy>
    {
    }
}
