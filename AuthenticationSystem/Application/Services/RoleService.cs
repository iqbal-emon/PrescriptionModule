using AuthenticationSystem.Domain.Repositories.Role;
using AuthenticationSystem.Dtos.RequestDto.RoleDto;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace AuthenticationSystem.Application.Services
{
    public class RoleService
    {
        private readonly IRoleQueryRepository _roleQueryRepository;
        private readonly IRoleCommandRepository _roleCommandRepository;
        private readonly MapperService _mapperService;

        public RoleService(IRoleQueryRepository roleQueryRepository, IRoleCommandRepository roleCommandRepository,
            MapperService mapperService)
        {
            _roleQueryRepository = roleQueryRepository;
            _roleCommandRepository = roleCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Role>>> GetAll()
        {
            var response = new Response<List<Role>>();
            try
            {
                var roles = await _roleQueryRepository.GetAll();
                if (roles == null)
                {
                    ResponseHelper.SetFailedResponse<List<Role>>(response, roles.Result, roles.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<Role>>(response, roles.Result, roles.Message, StatusResponseMessage.success, roles.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the roles.";
                ResponseHelper.SetFailedResponse<List<Role>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<Role>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Role>> GetById(int id)
        {
            var response = new Response<Role>();
            try
            {
                var role = await _roleQueryRepository.GetById(id);
                if (role == null)
                {
                    ResponseHelper.SetFailedResponse(response, role.Result, role.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, role.Result, role.Message, StatusResponseMessage.success, role.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the role.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(RoleInsertRequestDto roleDto)
        {
            var response = new Response<int>();
            try
            {
                var roleEntity = await _mapperService.MapSingle<RoleInsertRequestDto, Role>(roleDto);
                var insertResponse = await _roleCommandRepository.Insert(roleEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the role.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the role.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(RoleUpdateRequestDto roleDto)
        {
            var response = new Response<int>();
            try
            {
                var roleEntity = await _mapperService.MapSingle<RoleUpdateRequestDto, Role>(roleDto);
                var updatedResponse = await _roleCommandRepository.Update(roleEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the role.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the role.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _roleCommandRepository.Delete(id);
        }
    }
}

