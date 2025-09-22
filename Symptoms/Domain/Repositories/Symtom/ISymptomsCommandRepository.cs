using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace symtoms.Domain.Repositories.Systom
{
    public interface ISymptomsCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.Symptom>
    {
    }
}
