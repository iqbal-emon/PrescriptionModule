
using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionAdvice;
using Prescription.Dtos.RequestDto.PrescriptionAdvice;
using Prescription.Dtos.ResponseDto.PrescriptionAdvice;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace Prescription.Application.Services
{
    public class PrescriptionAdviceService
    {
        private readonly IPrescriptionAdviceQueryRepository _prescriptionAdviceQueryRepository;
        private readonly IPrescriptionAdviceCommandRepository _prescriptionAdviceCommandRepository;
        private readonly MapperService _mapperService;

        public PrescriptionAdviceService(
            IPrescriptionAdviceQueryRepository prescriptionAdviceQueryRepository,
            IPrescriptionAdviceCommandRepository prescriptionAdviceCommandRepository,
            MapperService mapperService)
        {
            _prescriptionAdviceQueryRepository = prescriptionAdviceQueryRepository;
            _prescriptionAdviceCommandRepository = prescriptionAdviceCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>>();

            try
            {
                var prescriptionAdvices = await _prescriptionAdviceQueryRepository.GetAll();

                if (prescriptionAdvices == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionAdvices.Result, prescriptionAdvices.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionAdvices.Result, prescriptionAdvices.Message, StatusResponseMessage.success, prescriptionAdvices.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescription advice.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>();

            try
            {
                var prescriptionAdvice = await _prescriptionAdviceQueryRepository.GetById(id);

                if (prescriptionAdvice == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionAdvice.Result, prescriptionAdvice.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionAdvice.Result, prescriptionAdvice.Message, StatusResponseMessage.success, prescriptionAdvice.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescription advice.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }



        public async Task<Response<List<PrescriptionAdviceResponseDto>>> GetByPrescriptionId(int id)
        {
            var response = new Response<List<PrescriptionAdviceResponseDto>>();
            try
            {
                var prescriptionAdvices = await _prescriptionAdviceQueryRepository.GetByPrescriptionId(id);
                if (prescriptionAdvices == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionAdvices.Result, prescriptionAdvices.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionAdvices.Result, prescriptionAdvices.Message, StatusResponseMessage.success, prescriptionAdvices.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescription advice.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(PrescriptionAdviceInsertRequestDto prescriptionAdvice)
        {
            var response = new Response<int>();

            try
            {
                var prescriptionAdviceEntity = await _mapperService.MapSingle<PrescriptionAdviceInsertRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>(prescriptionAdvice);

                var insertResponse = await _prescriptionAdviceCommandRepository.Insert(prescriptionAdviceEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the prescription advice.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the prescription advice.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(PrescriptionAdviceUpdateRequestDto prescriptionAdvice)
        {
            var response = new Response<int>();

            try
            {
                var prescriptionAdviceEntity = await _mapperService.MapSingle<PrescriptionAdviceUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>(prescriptionAdvice);
                prescriptionAdviceEntity.UpdatedAt = DateTime.Now;
                prescriptionAdviceEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _prescriptionAdviceCommandRepository.Update(prescriptionAdviceEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the prescription advice.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the prescription advice.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionAdviceCommandRepository.Delete(id);
        }
    }
}
