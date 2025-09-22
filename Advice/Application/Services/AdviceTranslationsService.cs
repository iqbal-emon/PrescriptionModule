using Advice.Domain.Repositories.AdviceTranslations;
using Advice.Dtos.RequestDto.AdviceTranslationsDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace Advice.Application.Services
{
    public class AdviceTranslationsService
    {
        private readonly IAdviceTranslationsQueryRepository _adviceTranslationsQueryRepository;
        private readonly IAdviceTranslationsCommandRepository _adviceTranslationsCommandRepository;
        private readonly MapperService _mapperService;

        public AdviceTranslationsService(IAdviceTranslationsQueryRepository adviceTranslationsQueryRepository, IAdviceTranslationsCommandRepository adviceTranslationsCommandRepository,
            MapperService mapperService)
        {
            _adviceTranslationsQueryRepository = adviceTranslationsQueryRepository;
            _adviceTranslationsCommandRepository = adviceTranslationsCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.AdviceTranslation>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.AdviceTranslation>>();

            try
            {
                var adviceTranslations = await _adviceTranslationsQueryRepository.GetAll();

                if (adviceTranslations == null)
                {
                    ResponseHelper.SetFailedResponse(response, adviceTranslations.Result, adviceTranslations.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, adviceTranslations.Result, adviceTranslations.Message, StatusResponseMessage.success, adviceTranslations.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the advice translations.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.AdviceTranslation>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.AdviceTranslation>();

            try
            {
                var adviceTranslation = await _adviceTranslationsQueryRepository.GetById(id);

                if (adviceTranslation == null)
                {
                    ResponseHelper.SetFailedResponse(response, adviceTranslation.Result, adviceTranslation.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, adviceTranslation.Result, adviceTranslation.Message, StatusResponseMessage.success, adviceTranslation.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the advice translation.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(AdviceTranslationsInsertRequestDto adviceTranslation)
        {
            var response = new Response<int>();

            try
            {
                var adviceTranslationEntity = await _mapperService.MapSingle<AdviceTranslationsInsertRequestDto, Entities.EntityClass.AdviceTranslation>(adviceTranslation);

                var insertResponse = await _adviceTranslationsCommandRepository.Insert(adviceTranslationEntity);

                response = insertResponse;

            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the advice translation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the advice translation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(AdviceTranslationsUpdateRequestDto adviceTranslation)
        {
            var response = new Response<int>();

            try
            {
                var adviceTranslationEntity = await _mapperService.MapSingle<AdviceTranslationsUpdateRequestDto, Entities.EntityClass.AdviceTranslation>(adviceTranslation);
                adviceTranslationEntity.UpdatedAt = DateTime.Now;
                adviceTranslationEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _adviceTranslationsCommandRepository.Update(adviceTranslationEntity);
                response = updatedResponse;



                //ResponseHelper.SetSuccessResponse(response, updatedResponse.Result, updatedResponse.Message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the advice translation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the advice translation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _adviceTranslationsCommandRepository.Delete(id);
        }
    }
}
