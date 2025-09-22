using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Diseases.Domain.Repositories.Diseases
{
    public interface  IDiseasesCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.Disease>
    {
    }
}


