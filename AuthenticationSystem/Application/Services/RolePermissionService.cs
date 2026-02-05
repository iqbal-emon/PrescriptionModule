using AuthenticationSystem.Domain.Repositories.RolePermission;
using AuthenticationSystem.Dtos.RequestDto.RolePermissionDto;
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
    public class RolePermissionService
    {
        private readonly IRolePermissionQueryRepository _rolePermissionQueryRepository;
        private readonly IRolePermissionCommandRepository _rolePermissionCommandRepository;
        private readonly MapperService _mapperService;

        public RolePermissionService(IRolePermissionQueryRepository rolePermissionQueryRepository, IRolePermissionCommandRepository rolePermissionCommandRepository,
            MapperService mapperService)
        {
            _rolePermissionQueryRepository = rolePermissionQueryRepository;
            _rolePermissionCommandRepository = rolePermissionCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<RolePermission>>> GetAll()
        {
            var response = new Response<List<RolePermission>>();
            try
            {
                var rolePermissions = await _rolePermissionQueryRepository.GetAll();
                if (rolePermissions == null)
                {
                    ResponseHelper.SetFailedResponse<List<RolePermission>>(response, rolePermissions.Result, rolePermissions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<RolePermission>>(response, rolePermissions.Result, rolePermissions.Message, StatusResponseMessage.success, rolePermissions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the role permissions.";
                ResponseHelper.SetFailedResponse<List<RolePermission>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<RolePermission>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<RolePermission>> GetById(int id)
        {
            var response = new Response<RolePermission>();
            try
            {
                var rolePermission = await _rolePermissionQueryRepository.GetById(id);
                if (rolePermission == null)
                {
                    ResponseHelper.SetFailedResponse(response, rolePermission.Result, rolePermission.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, rolePermission.Result, rolePermission.Message, StatusResponseMessage.success, rolePermission.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the role permission.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<List<RolePermission>>> GetByRoleId(int roleId)
        {
            var response = new Response<List<RolePermission>>();
            try
            {
                var rolePermissions = await _rolePermissionQueryRepository.GetByRoleId(roleId);
                if (rolePermissions == null)
                {
                    ResponseHelper.SetFailedResponse<List<RolePermission>>(response, rolePermissions.Result, rolePermissions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<RolePermission>>(response, rolePermissions.Result, rolePermissions.Message, StatusResponseMessage.success, rolePermissions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the role permissions.";
                ResponseHelper.SetFailedResponse<List<RolePermission>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<RolePermission>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<List<RolePermission>>> GetByPermissionId(int permissionId)
        {
            var response = new Response<List<RolePermission>>();
            try
            {
                var rolePermissions = await _rolePermissionQueryRepository.GetByPermissionId(permissionId);
                if (rolePermissions == null)
                {
                    ResponseHelper.SetFailedResponse<List<RolePermission>>(response, rolePermissions.Result, rolePermissions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<RolePermission>>(response, rolePermissions.Result, rolePermissions.Message, StatusResponseMessage.success, rolePermissions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the role permissions.";
                ResponseHelper.SetFailedResponse<List<RolePermission>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<RolePermission>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(RolePermissionInsertRequestDto rolePermissionDto)
        {
            var response = new Response<int>();
            try
            {
                var rolePermissionEntity = await _mapperService.MapSingle<RolePermissionInsertRequestDto, RolePermission>(rolePermissionDto);
                var insertResponse = await _rolePermissionCommandRepository.Insert(rolePermissionEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the role permission.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the role permission.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(RolePermissionUpdateRequestDto rolePermissionDto)
        {
            var response = new Response<int>();
            try
            {
                var rolePermissionEntity = await _mapperService.MapSingle<RolePermissionUpdateRequestDto, RolePermission>(rolePermissionDto);
                var updatedResponse = await _rolePermissionCommandRepository.Update(rolePermissionEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the role permission.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the role permission.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _rolePermissionCommandRepository.Delete(id);
        }
    }
}

