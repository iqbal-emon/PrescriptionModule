using AuthenticationSystem.Domain.Repositories.User;
using AuthenticationSystem.Dtos.RequestDto.UserDto;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace AuthenticationSystem.Application.Services
{
    public class UserService
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly MapperService _mapperService;

        public UserService(IUserQueryRepository userQueryRepository, IUserCommandRepository userCommandRepository,
            MapperService mapperService)
        {
            _userQueryRepository = userQueryRepository;
            _userCommandRepository = userCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.User>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.User>>();
            try
            {
                var users = await _userQueryRepository.GetAll();
                if (users == null)
                {
                    ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, users.Result, users.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<Entities.EntityClass.User>>(response, users.Result, users.Message, StatusResponseMessage.success, users.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the users.";
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.User>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.User>();
            try
            {
                var user = await _userQueryRepository.GetById(id);
                if (user == null)
                {
                    ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, user.Result, user.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<Entities.EntityClass.User>(response, user.Result, user.Message, StatusResponseMessage.success, user.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the user.";
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.User>> GetByEmail(string email)
        {
            var response = new Response<Entities.EntityClass.User>();
            try
            {
                var user = await _userQueryRepository.GetByEmail(email);
                if (user == null)
                {
                    ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, user.Result, user.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<Entities.EntityClass.User>(response, user.Result, user.Message, StatusResponseMessage.success, user.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the user.";
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.User>>> GetByTenantId(int tenantId)
        {
            var response = new Response<List<Entities.EntityClass.User>>();
            try
            {
                var users = await _userQueryRepository.GetByTenantId(tenantId);
                if (users == null)
                {
                    ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, users.Result, users.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<Entities.EntityClass.User>>(response, users.Result, users.Message, StatusResponseMessage.success, users.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the users.";
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.User>>> GetByUserType(string userType)
        {
            var response = new Response<List<Entities.EntityClass.User>>();
            try
            {
                var users = await _userQueryRepository.GetByUserType(userType);
                if (users == null)
                {
                    ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, users.Result, users.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<Entities.EntityClass.User>>(response, users.Result, users.Message, StatusResponseMessage.success, users.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the users.";
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.User>> GetByUserName(string userName)
        {
            var response = new Response<Entities.EntityClass.User>();
            try
            {
                var user = await _userQueryRepository.GetByUserName(userName);
                if (user == null)
                {
                    ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, user.Result, user.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<Entities.EntityClass.User>(response, user.Result, user.Message, StatusResponseMessage.success, user.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the user.";
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.User>> GetByPhoneNo(string phoneNo)
        {
            var response = new Response<Entities.EntityClass.User>();
            try
            {
                var user = await _userQueryRepository.GetByPhoneNo(phoneNo);
                if (user == null)
                {
                    ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, user.Result, user.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<Entities.EntityClass.User>(response, user.Result, user.Message, StatusResponseMessage.success, user.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the user.";
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.User>>> GetByRoleId(int roleId)
        {
            var response = new Response<List<Entities.EntityClass.User>>();
            try
            {
                var users = await _userQueryRepository.GetByRoleId(roleId);
                if (users == null)
                {
                    ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, users.Result, users.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<Entities.EntityClass.User>>(response, users.Result, users.Message, StatusResponseMessage.success, users.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the users.";
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(UserInsertRequestDto userDto)
        {
            var response = new Response<int>();
            try
            {
                var userEntity = await _mapperService.MapSingle<UserInsertRequestDto, Entities.EntityClass.User>(userDto);
                var insertResponse = await _userCommandRepository.Insert(userEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the user.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the user.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> InsertWithStoredProcedureDto(UserInsertStoredProcedureDto dto)
        {
            var response = new Response<int>();
            try
            {
                var insertResponse = await _userCommandRepository.InsertWithDto(dto);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the user.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the user.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(UserUpdateRequestDto userDto)
        {
            var response = new Response<int>();
            try
            {
                var userEntity = await _mapperService.MapSingle<UserUpdateRequestDto, Entities.EntityClass.User>(userDto);
                userEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _userCommandRepository.Update(userEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the user.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the user.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _userCommandRepository.Delete(id);
        }
    }
}
