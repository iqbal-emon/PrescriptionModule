using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionSymtom;
using Prescription.Dtos.RequestDto.PrescriptionSymtomDto;
using Prescription.Dtos.ResponseDto.PrescriptionSymtomDto;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace Prescription.Application.Services
{
    public class PrescriptionSymptomService
    {
        private readonly IPrescriptionSymtomQueryRepository _prescriptionSymptomQueryRepository;
        private readonly IPrescriptionSymtomCommandRepository _prescriptionSymptomCommandRepository;
        private readonly MapperService _mapperService;

        public PrescriptionSymptomService(
            IPrescriptionSymtomQueryRepository prescriptionSymptomQueryRepository,
            IPrescriptionSymtomCommandRepository prescriptionSymptomCommandRepository,
            MapperService mapperService)
        {
            _prescriptionSymptomQueryRepository = prescriptionSymptomQueryRepository;
            _prescriptionSymptomCommandRepository = prescriptionSymptomCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>>();

            try
            {
                var symptoms = await _prescriptionSymptomQueryRepository.GetAll();

                if (symptoms == null)
                {
                    ResponseHelper.SetFailedResponse(response, symptoms.Result, symptoms.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, symptoms.Result, symptoms.Message, StatusResponseMessage.success, symptoms.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescription symptoms.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>();

            try
            {
                var symptom = await _prescriptionSymptomQueryRepository.GetById(id);

                if (symptom == null)
                {
                    ResponseHelper.SetFailedResponse(response, symptom.Result, symptom.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, symptom.Result, symptom.Message, StatusResponseMessage.success, symptom.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescription symptom.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
        public async Task<Response<List<PrescriptionSymtomResponseDto>>> GetByPrescriptionId(int id)
        {
            var response = new Response<List<PrescriptionSymtomResponseDto>>();
            try
            {
                var symptoms = await _prescriptionSymptomQueryRepository.GetByPrescriptionId(id);
                if (symptoms == null)
                {
                    ResponseHelper.SetFailedResponse(response, symptoms.Result, symptoms.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, symptoms.Result, symptoms.Message, StatusResponseMessage.success, symptoms.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the prescription symptom.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            return response;
        }

        public async Task<Response<int>> Insert(PrescriptionSymtomInsertRequestDto prescriptionSymptom)
        {
            var response = new Response<int>();

            try
            {
                var symptomEntity = await _mapperService.MapSingle<PrescriptionSymtomInsertRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>(prescriptionSymptom);
                var insertResponse = await _prescriptionSymptomCommandRepository.Insert(symptomEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the prescription symptom.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the prescription symptom.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(PrescriptionSymtomUpdateRequestDto prescriptionSymptom)
        {
            var response = new Response<int>();

            try
            {
                var symptomEntity = await _mapperService.MapSingle<PrescriptionSymtomUpdateRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionSymtom>(prescriptionSymptom);
                symptomEntity.UpdatedAt = DateTime.Now;
                symptomEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _prescriptionSymptomCommandRepository.Update(symptomEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the prescription symptom.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the prescription symptom.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionSymptomCommandRepository.Delete(id);
        }

        
    }
}