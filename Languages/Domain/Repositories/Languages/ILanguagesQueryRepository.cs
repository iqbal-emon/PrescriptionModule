using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Languages.Domain.Repositories.Languages
{
    public interface  ILanguagesQueryRepository: IBaseCommonQueryMethodRepository<Entities.EntityClass.Language>
    {
    }
}
