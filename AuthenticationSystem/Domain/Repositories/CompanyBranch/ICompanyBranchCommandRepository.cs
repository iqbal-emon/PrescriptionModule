using Entities.EntityClass.CompanyEntity;
using CompanyBranchEntity = Entities.EntityClass.CompanyEntity.CompanyBranch;
using Utility.BaseInterface;

namespace AuthenticationSystem.Domain.Repositories.CompanyBranch
{
    public interface ICompanyBranchCommandRepository : IBaseCommonCommandMethodRepository<CompanyBranchEntity>
    {
    }
}

