using Utility.BaseInterface;
using Utility.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationSystem.Domain.Repositories.CompanyBranch
{
    public interface ICompanyBranchQueryRepository : IBaseCommonQueryMethodRepository<global::Entities.EntityClass.CompanyEntity.CompanyBranch>
    {
        Task<Response<List<global::Entities.EntityClass.CompanyEntity.CompanyBranch>>> GetByCompanyId(int companyId);
    }
}

