using AuthenticationSystem.Dtos.RequestDto.UserDto;
using Entities.EntityClass;
using Utility.BaseInterface;
using Utility.Response;

namespace AuthenticationSystem.Domain.Repositories.User
{
    public interface IUserCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.User>
    {
        Task<Response<int>> InsertWithDto(UserInsertStoredProcedureDto dto);
    }
}

