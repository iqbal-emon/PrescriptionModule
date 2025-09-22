using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace FollowUp.Domain.Repositories.FollowUp
{
    public interface IFollowUpQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.FollowUp>
    {
        Task<Response<List<Entities.EntityClass.FollowUp>>> GetAllFollowUpByName(string followUpName);
        Task<Response<List<Entities.EntityClass.FollowUp>>> GetBookMarks();


        
    }
}
