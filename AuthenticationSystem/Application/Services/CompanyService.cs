using AuthenticationSystem.Domain.Repositories.Company;
using AuthenticationSystem.Dtos.RequestDto.CompanyDto;
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
    public class CompanyService
    {
        private readonly ICompanyQueryRepository _companyQueryRepository;
        private readonly ICompanyCommandRepository _companyCommandRepository;
        private readonly MapperService _mapperService;

        public CompanyService(ICompanyQueryRepository companyQueryRepository, ICompanyCommandRepository companyCommandRepository,
            MapperService mapperService)
        {
            _companyQueryRepository = companyQueryRepository;
            _companyCommandRepository = companyCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Company>>> GetAll()
        {
            var response = new Response<List<Company>>();
            try
            {
                var companies = await _companyQueryRepository.GetAll();
                if (companies == null)
                {
                    ResponseHelper.SetFailedResponse<List<Company>>(response, companies.Result, companies.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse<List<Company>>(response, companies.Result, companies.Message, StatusResponseMessage.success, companies.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the companies.";
                ResponseHelper.SetFailedResponse<List<Company>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse<List<Company>>(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<Company>> GetById(int id)
        {
            var response = new Response<Company>();
            try
            {
                var company = await _companyQueryRepository.GetById(id);
                if (company == null)
                {
                    ResponseHelper.SetFailedResponse(response, company.Result, company.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, company.Result, company.Message, StatusResponseMessage.success, company.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the company.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(CompanyInsertRequestDto companyDto)
        {
            var response = new Response<int>();
            try
            {
                var companyEntity = await _mapperService.MapSingle<CompanyInsertRequestDto, Company>(companyDto);
                var insertResponse = await _companyCommandRepository.Insert(companyEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the company.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the company.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(CompanyUpdateRequestDto companyDto)
        {
            var response = new Response<int>();
            try
            {
                var companyEntity = await _mapperService.MapSingle<CompanyUpdateRequestDto, Company>(companyDto);
                companyEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _companyCommandRepository.Update(companyEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the company.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the company.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _companyCommandRepository.Delete(id);
        }
    }
}

