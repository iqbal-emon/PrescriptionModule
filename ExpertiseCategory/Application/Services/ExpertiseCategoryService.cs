using ExpertiseCategory.Domain.Repositories.ExpertiseCategory;
using ExpertiseCategory.Dtos.RequestDto.ExpertiseCategoryDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace ExpertiseCategory.Application.Services
{
    public class ExpertiseCategoryService
    {
        private readonly IExpertiseCategoryQueryRepository _expertiseCategoryQueryRepository;
        private readonly IExpertiseCategoryCommandRepository _expertiseCategoryCommandRepository;
        private readonly MapperService _mapperService;

        public ExpertiseCategoryService(
            IExpertiseCategoryQueryRepository expertiseCategoryQueryRepository,
            IExpertiseCategoryCommandRepository expertiseCategoryCommandRepository,
            MapperService mapperService)
        {
            _expertiseCategoryQueryRepository = expertiseCategoryQueryRepository;
            _expertiseCategoryCommandRepository = expertiseCategoryCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.ExpertiseCategory>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.ExpertiseCategory>>();

            try
            {
                var categories = await _expertiseCategoryQueryRepository.GetAll();

                if (categories == null)
                {
                    ResponseHelper.SetFailedResponse(response, categories.Result, categories.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, categories.Result, categories.Message, StatusResponseMessage.success, categories.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the ExpertiseCategories.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.ExpertiseCategory>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.ExpertiseCategory>();

            try
            {
                var category = await _expertiseCategoryQueryRepository.GetById(id);

                if (category == null)
                {
                    ResponseHelper.SetFailedResponse(response, category.Result, category.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, category.Result, category.Message, StatusResponseMessage.success, category.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the ExpertiseCategory.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(ExpertiseCategoryInsertRequestDto category)
        {
            var response = new Response<int>();

            try
            {
                var categoryEntity = await _mapperService.MapSingle<ExpertiseCategoryInsertRequestDto, Entities.EntityClass.DoctorEntity.ExpertiseCategory>(category);
                categoryEntity.CreatedAt = DateTime.Today;
                var insertResponse = await _expertiseCategoryCommandRepository.Insert(categoryEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the ExpertiseCategory.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the ExpertiseCategory.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(ExpertiseCategoryUpdateRequestDto category)
        {
            var response = new Response<int>();

            try
            {
                var categoryEntity = await _mapperService.MapSingle<ExpertiseCategoryUpdateRequestDto, Entities.EntityClass.DoctorEntity.ExpertiseCategory>(category);
                categoryEntity.UpdatedAt = DateTime.Now;
                categoryEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _expertiseCategoryCommandRepository.Update(categoryEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the ExpertiseCategory.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the ExpertiseCategory.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _expertiseCategoryCommandRepository.Delete(id);
        }
    }
}