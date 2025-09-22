using Languages.Domain.Repositories.Languages;
using Languages.Dtos.RequestDto.LanguagesDto;
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

namespace Languages.Application.Services
{
    public class LanguagesService
    {
        private readonly ILanguagesQueryRepository _languagesQueryRepository;
        private readonly ILanguagesCommandRepository _languagesCommandRepository;
        private readonly MapperService _mapperService;
        public LanguagesService(ILanguagesQueryRepository languagesQueryRepository, ILanguagesCommandRepository languagesCommandRepository,
            MapperService mapperService)
        {
            _languagesQueryRepository = languagesQueryRepository;
            _languagesCommandRepository = languagesCommandRepository;
            _mapperService = mapperService;
        }
        public async Task<Response<List<Entities.EntityClass.Language>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Language>>();

            try
            {
                var languages = await _languagesQueryRepository.GetAll();

                if (languages == null)
                {
                    ResponseHelper.SetFailedResponse(response, languages.Result, languages.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, languages.Result, languages.Message, StatusResponseMessage.success, languages.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the languages.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
        public async Task<Response<Entities.EntityClass.Language>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Language>();

            try
            {
                var language = await _languagesQueryRepository.GetById(id);

                if (language == null)
                {
                    ResponseHelper.SetFailedResponse(response, language.Result, language.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, language.Result, language.Message, StatusResponseMessage.success, language.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the language.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }


        public async Task<Response<int>> Insert(LanguagesInsertRequestDto language)
        {
            var response = new Response<int>();

            try
            {
                var languageEntity = await _mapperService.MapSingle<LanguagesInsertRequestDto, Entities.EntityClass.Language>(language);

                var insertResponse = await _languagesCommandRepository.Insert(languageEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the language.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the language.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Update(LanguagesUpdateRequestDto language)
        {

            var response = new Response<int>();

            try
            {
                var languageEntity = await _mapperService.MapSingle<LanguagesUpdateRequestDto, Entities.EntityClass.Language>(language);
                languageEntity.UpdatedAt = DateTime.Now;
                languageEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _languagesCommandRepository.Update(languageEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the language.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);

            }
            catch (Exception ex)
            {
                response.Message = "A database error occurred while updating the language.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);

            }

            return response;

        }
        public Task<Response<bool>> Delete(int id)
        {
            return _languagesCommandRepository.Delete(id);
        }

    }
}