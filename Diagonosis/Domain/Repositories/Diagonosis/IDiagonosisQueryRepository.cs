using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Diagonosis.Domain.Repositories.Diagonosis
{
    public interface IDiagonosisQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.Diagonosis>
    {
        Task<Response<List<Entities.EntityClass.Diagonosis>>> GetAllDiagononosisName(string diagnonsisName);
        Task<Response<List<Entities.EntityClass.Diagonosis>>> GetBookMarks(int doctorId);

        
    }
}
