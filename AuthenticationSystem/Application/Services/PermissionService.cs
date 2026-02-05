using AuthenticationSystem.Domain.Repositories.Permission;
using AuthenticationSystem.Dtos.RequestDto.PermissionDto;
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
    public class PermissionService
    {
        private readonly IPermissionQueryRepository _permissionQueryRepository;
        private readonly IPermissionCommandRepository _permissionCommandRepository;
        private readonly MapperService _mapperService;

        public PermissionService(IPermissionQueryRepository permissionQueryRepository, IPermissionCommandRepository permissionCommandRepository,
            MapperService mapperService)
        {
            _permissionQueryRepository = permissionQueryRepository;
            _permissionCommandRepository = permissionCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Permission>>> GetAll()
        {
            var response = new Response<List<Permission>>();
            try
            {
                var permissions = await _permissionQueryRepository.GetAll();
                if (permissions == null)
                {
                    ResponseHelper.SetFailedResponse<List<Permission>>(response, permissions.Result, permissions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<Permission>>(response, permissions.Result, permissions.Message, StatusResponseMessage.success, permissions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the permissions.";
                ResponseHelper.SetFailedResponse<List<Permission>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<Permission>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Permission>> GetById(int id)
        {
            var response = new Response<Permission>();
            try
            {
                var permission = await _permissionQueryRepository.GetById(id);
                if (permission == null)
                {
                    ResponseHelper.SetFailedResponse(response, permission.Result, permission.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, permission.Result, permission.Message, StatusResponseMessage.success, permission.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the permission.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(PermissionInsertRequestDto permissionDto)
        {
            var response = new Response<int>();
            try
            {
                var permissionEntity = await _mapperService.MapSingle<PermissionInsertRequestDto, Permission>(permissionDto);
                var insertResponse = await _permissionCommandRepository.Insert(permissionEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the permission.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the permission.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(PermissionUpdateRequestDto permissionDto)
        {
            var response = new Response<int>();
            try
            {
                var permissionEntity = await _mapperService.MapSingle<PermissionUpdateRequestDto, Permission>(permissionDto);
                var updatedResponse = await _permissionCommandRepository.Update(permissionEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the permission.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the permission.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _permissionCommandRepository.Delete(id);
        }
    }
}
