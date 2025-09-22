using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace DoctorChamber.Domain.Repositories.DoctorChamber
{
    public interface IDoctorChamberCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DoctorEntity.DoctorChamber>
    {
        
    }
}
