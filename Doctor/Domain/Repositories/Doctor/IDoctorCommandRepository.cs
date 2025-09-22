using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Doctor.Domain.Repositories.Doctor
{
    public interface IDoctorCommandRepository: IBaseCommonCommandMethodRepository<Entities.EntityClass.Doctor>
    {
    }
}
