using AuthenticationSystem.Domain.Repositories.CompanyBranch;
using AuthenticationSystem.Dtos.RequestDto.CompanyBranchDto;
using Entities.EntityClass.CompanyEntity;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace AuthenticationSystem.Application.Services
{
    public class CompanyBranchService
    {
        private readonly ICompanyBranchQueryRepository _companyBranchQueryRepository;
        private readonly ICompanyBranchCommandRepository _companyBranchCommandRepository;
        private readonly MapperService _mapperService;

        public CompanyBranchService(ICompanyBranchQueryRepository companyBranchQueryRepository, ICompanyBranchCommandRepository companyBranchCommandRepository,
            MapperService mapperService)
        {
            _companyBranchQueryRepository = companyBranchQueryRepository;
            _companyBranchCommandRepository = companyBranchCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<CompanyBranch>>> GetAll()
        {
            var response = new Response<List<CompanyBranch>>();
            try
            {
                var branches = await _companyBranchQueryRepository.GetAll();
                if (branches == null)
                {
                    ResponseHelper.SetFailedResponse<List<CompanyBranch>>(response, branches.Result, branches.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<CompanyBranch>>(response, branches.Result, branches.Message, StatusResponseMessage.success, branches.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the company branches.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<CompanyBranch>> GetById(int id)
        {
            var response = new Response<CompanyBranch>();
            try
            {
                var branch = await _companyBranchQueryRepository.GetById(id);
                if (branch == null)
                {
                    ResponseHelper.SetFailedResponse(response, branch.Result, branch.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, branch.Result, branch.Message, StatusResponseMessage.success, branch.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the company branch.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<List<CompanyBranch>>> GetByCompanyId(int companyId)
        {
            var response = new Response<List<CompanyBranch>>();
            try
            {
                var branches = await _companyBranchQueryRepository.GetByCompanyId(companyId);
                if (branches == null)
                {
                    ResponseHelper.SetFailedResponse<List<CompanyBranch>>(response, branches.Result, branches.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<CompanyBranch>>(response, branches.Result, branches.Message, StatusResponseMessage.success, branches.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the company branches.";
                ResponseHelper.SetFailedResponse<List<CompanyBranch>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<CompanyBranch>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(CompanyBranchInsertRequestDto branchDto)
        {
            var response = new Response<int>();
            try
            {
                var branchEntity = await _mapperService.MapSingle<CompanyBranchInsertRequestDto, CompanyBranch>(branchDto);
                var insertResponse = await _companyBranchCommandRepository.Insert(branchEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the company branch.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the company branch.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(CompanyBranchUpdateRequestDto branchDto)
        {
            var response = new Response<int>();
            try
            {
                var branchEntity = await _mapperService.MapSingle<CompanyBranchUpdateRequestDto, CompanyBranch>(branchDto);
                branchEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _companyBranchCommandRepository.Update(branchEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the company branch.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the company branch.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _companyBranchCommandRepository.Delete(id);
        }
    }
}

