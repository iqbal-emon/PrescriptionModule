using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace CommonHistory.Domain.Repositories.CommonHistory
{
    public interface ICommonHistoryQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.CommonHistory>
    {

        Task<Response<List<Entities.EntityClass.CommonHistory>>> GetAllCommonHistoryByName(string commonHistoryName);

        Task<Response<List<Entities.EntityClass.CommonHistory>>> GetBookMarks(int doctorId);

        
    }
}
