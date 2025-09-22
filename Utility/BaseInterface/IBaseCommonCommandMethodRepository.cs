using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace Utility.BaseInterface
{
    public interface IBaseCommonCommandMethodRepository<TEntity> where TEntity : class
    {
        Task<Response<int>> Insert(TEntity entity);
        Task<Response<int>> Update(TEntity entity);
        Task<Response<bool>> Delete(int id);
    }
}
