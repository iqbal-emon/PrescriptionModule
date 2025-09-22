using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace Utility.BaseInterface
{
    public interface IBaseCommonQueryMethodRepository<TEntity> where TEntity : class
    {
        Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new();
        Task<Response<List<TEntity>>> GetAll();
        public Task<Response<TEntity>> GetById(int id);
    }
}
