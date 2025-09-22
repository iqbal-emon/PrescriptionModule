using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Advice.Domain.Repositories.Advice
{
    public interface IAdviceQueryRepository: IBaseCommonQueryMethodRepository<Entities.EntityClass.CommonAdvice>
    {
        Task<Response<List<Entities.EntityClass.CommonAdvice>>> GetAllAdviceByName(string commonHistoryName);
        Task<Response<List<Entities.EntityClass.CommonAdvice>>> GetBookMarks(int doctorId);

    }
}
