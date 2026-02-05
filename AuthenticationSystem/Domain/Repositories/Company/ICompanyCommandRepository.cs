using Entities.EntityClass.CompanyEntity;
using Utility.BaseInterface;

namespace AuthenticationSystem.Domain.Repositories.Company
{
    public interface ICompanyCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.CompanyEntity.Company>
    {
    }
}

