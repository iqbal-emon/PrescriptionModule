using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;
using User.Dtos.RequestDto.UserDto;
using User.Domain.Repositories.User;
using Entities.EntityClass;
using SharedService.Model;
using System.Data;

namespace User.Application.Services
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

        // Get all users
        public async Task<Response<List<Entities.EntityClass.User>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.User>>();

            try
            {
                var users = await _userQueryRepository.GetAll();

                if (users == null)
                {
                    ResponseHelper.SetFailedResponse(response, users.Result, users.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, users.Result, users.Message, StatusResponseMessage.success, users.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the users.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Get user by ID
        public async Task<Response<Entities.EntityClass.User>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.User>();

            try
            {
                var user = await _userQueryRepository.GetById(id);

                if (user == null)
                {
                    ResponseHelper.SetFailedResponse(response, user.Result, user.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, user.Result, user.Message, StatusResponseMessage.success, user.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the user.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Insert a new user
        public async Task<Response<int>> Insert(UserInsertRequestDto user)
        {
            var response = new Response<int>();

            try
            {
                var userEntity = await _mapperService.MapSingle<UserInsertRequestDto, Entities.EntityClass.User>(user);

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


        public async Task<Response<Entities.EntityClass.User>> GetByRoleAndReferenceId(int referenceId,string role,int tenantId)
        {
            var response = new Response<Entities.EntityClass.User>();

            try
            {
                var user = await _userQueryRepository.GetByRoleAndReferenceId(referenceId,role, tenantId);

                if (user == null)
                {
                    ResponseHelper.SetFailedResponse(response, user.Result, user.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, user.Result, user.Message, StatusResponseMessage.success, user.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the user.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }



        // Update an existing user
        public async Task<Response<int>> Update(UserUpdateRequestDto user)
        {
            var response = new Response<int>();

            try
            {
                var userEntity = await _mapperService.MapSingle<UserUpdateRequestDto, Entities.EntityClass.User>(user);
                userEntity.UpdatedAt = DateTime.Now;
                userEntity.CreatedAt = DateTime.Now;

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

        // Delete a user
        public Task<Response<bool>> Delete(int id)
        {
            return _userCommandRepository.Delete(id);
        }

        
    }
}
