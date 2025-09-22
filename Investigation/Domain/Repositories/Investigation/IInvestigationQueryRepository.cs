using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Investigation.Domain.Repositories.Investigation
{
    public interface IInvestigationQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.Investigation>
    {
        Task<Response<List<Entities.EntityClass.Investigation>>> GetAllInvestigationByName(string InvestigationName);
        Task<Response<List<Entities.EntityClass.Investigation>>> GetBookMarks();


    }
}
