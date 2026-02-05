
using Entities.EntityClass;
using Utility.BaseInterface;

namespace AuthenticationSystem.Domain.Repositories.User
{
    public interface IUserCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.User>
    {
    }
}

