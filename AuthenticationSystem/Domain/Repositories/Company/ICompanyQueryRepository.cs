using Entities.EntityClass.CompanyEntity;
using Utility.BaseInterface;
using Utility.Response;

namespace AuthenticationSystem.Domain.Repositories.Company
{
    public interface ICompanyQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.CompanyEntity.Company>
    {
    }
}

