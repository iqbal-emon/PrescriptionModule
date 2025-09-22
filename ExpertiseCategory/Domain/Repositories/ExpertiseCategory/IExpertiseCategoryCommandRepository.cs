using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace ExpertiseCategory.Domain.Repositories.ExpertiseCategory
{
    public interface IExpertiseCategoryCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DoctorEntity.ExpertiseCategory>
    {
        
    }
}
