using Examinations.Domain.Repositories.Examinations;
using Examinations.Dtos.RequestDto.ExaminationsDto;
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

namespace Examinations.Application.Services
{
    public class ExaminationsService
    {
        private readonly IExaminationsQueryRepository _examinationQueryRepository;
        private readonly IExaminationsCommandRepository _examinationCommandRepository;
        private readonly MapperService _mapperService;

        public ExaminationsService(
            IExaminationsQueryRepository examinationQueryRepository,
            IExaminationsCommandRepository examinationCommandRepository,
            MapperService mapperService)
        {
            _examinationQueryRepository = examinationQueryRepository;
            _examinationCommandRepository = examinationCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.Examination>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Examination>>();

            try
            {
                var examinations = await _examinationQueryRepository.GetAll();

                if (examinations == null)
                {
                    ResponseHelper.SetFailedResponse(response, examinations.Result, examinations.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, examinations.Result, examinations.Message, StatusResponseMessage.success, examinations.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the examinations.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Examination>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Examination>();

            try
            {
                var examination = await _examinationQueryRepository.GetById(id);

                if (examination == null)
                {
                    ResponseHelper.SetFailedResponse(response, examination.Result, examination.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, examination.Result, examination.Message, StatusResponseMessage.success, examination.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the examination.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(ExaminationsInsertRequestDto examination)
        {
            var response = new Response<int>();

            try
            {
                var examinationEntity = await _mapperService.MapSingle<ExaminationsInsertRequestDto, Entities.EntityClass.Examination>(examination);

                var insertResponse = await _examinationCommandRepository.Insert(examinationEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the examination.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the examination.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(ExaminationsUpdateRequestDto examination)
        {
            var response = new Response<int>();

            try
            {
                var examinationEntity = await _mapperService.MapSingle<ExaminationsUpdateRequestDto, Entities.EntityClass.Examination>(examination);
                examinationEntity.UpdatedAt = DateTime.Now;
                examinationEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _examinationCommandRepository.Update(examinationEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the examination.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the examination.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _examinationCommandRepository.Delete(id);
        }
    }
}
