using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionPatientHistory;
using Prescription.Dtos.RequestDto.PrescriptionPatientHistoryDto;
using Prescription.Dtos.ResponseDto.PrescriptionItemDto;
using Prescription.Dtos.ResponseDto.PrescriptionPatientHistory;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace Prescription.Application.Services
{
    public class PrescriptionPatientHistoryService
    {
        private readonly IPrescriptionPatientHistoryQueryRepository _prescriptionPatientHistoryQueryRepository;
        private readonly IPrescriptionPatientHistoryCommandRepository _prescriptionPatientHistoryCommandRepository;
        private readonly MapperService _mapperService;

        public PrescriptionPatientHistoryService(IPrescriptionPatientHistoryQueryRepository prescriptionPatientHistoryQueryRepository,
            IPrescriptionPatientHistoryCommandRepository prescriptionPatientHistoryCommandRepository,
            MapperService mapperService)
        {
            _prescriptionPatientHistoryQueryRepository = prescriptionPatientHistoryQueryRepository;
            _prescriptionPatientHistoryCommandRepository = prescriptionPatientHistoryCommandRepository;
            _mapperService = mapperService;
        }

        // Get all prescription patient history records
        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>>();

            try
            {
                var prescriptionHistories = await _prescriptionPatientHistoryQueryRepository.GetAll();

                if (prescriptionHistories == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionHistories.Result, prescriptionHistories.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionHistories.Result, prescriptionHistories.Message, StatusResponseMessage.success, prescriptionHistories.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription patient history records.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Get prescription patient history by ID
        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>();

            try
            {
                var prescriptionHistory = await _prescriptionPatientHistoryQueryRepository.GetById(id);

                if (prescriptionHistory == null)
                {
                    ResponseHelper.SetFailedResponse(response, prescriptionHistory.Result, prescriptionHistory.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, prescriptionHistory.Result, prescriptionHistory.Message, StatusResponseMessage.success, prescriptionHistory.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the prescription patient history record.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Insert a new prescription patient history record
        public async Task<Response<int>> Insert(PrescriptionPatientHistoryRequestDto prescriptionPatientHistory)
        {
            var response = new Response<int>();

            try
            {
                var prescriptionHistoryEntity = await _mapperService.MapSingle<PrescriptionPatientHistoryRequestDto, Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>(prescriptionPatientHistory);

                var insertResponse = await _prescriptionPatientHistoryCommandRepository.Insert(prescriptionHistoryEntity);
                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the prescription patient history record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the prescription patient history record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Update an existing prescription patient history record
        public async Task<Response<int>> Update(PrescriptionPatientHistoryUpdateDto prescriptionPatientHistory)
        {
            var response = new Response<int>();

            try
            {
                var prescriptionHistoryEntity = await _mapperService.MapSingle<PrescriptionPatientHistoryUpdateDto, Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>(prescriptionPatientHistory);
                prescriptionHistoryEntity.UpdatedAt = DateTime.Now;
                prescriptionHistoryEntity.CreatedAt = DateTime.Now;

                var updatedResponse = await _prescriptionPatientHistoryCommandRepository.Update(prescriptionHistoryEntity);
                response = updatedResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the prescription patient history record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the prescription patient history record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<PrescriptionCommonHistoryResponseDto>>> GetByPrescriptionId(int id)
        {
            var response = new Response<List<PrescriptionCommonHistoryResponseDto>>();

            try
            {
                var histories = await _prescriptionPatientHistoryQueryRepository.GetByPrescriptionId(id);

                if (histories == null)
                {
                    ResponseHelper.SetFailedResponse(response, histories.Result, histories.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, histories.Result, histories.Message, StatusResponseMessage.success, histories.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Prescription Diagnosis.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Delete a prescription patient history record
        public Task<Response<bool>> Delete(int id)
        {
            return _prescriptionPatientHistoryCommandRepository.Delete(id);
        }

        // Get all prescription patient history records by name
        public Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>>> GetAllPrescriptionPatientHistoryByName(string prescriptionHistoryName)
        {
            return _prescriptionPatientHistoryQueryRepository.GetAllPrescriptionPatientHistoryByName(prescriptionHistoryName);
        }

        // Get all prescription patient history records by name
        public Task<Response<List<Entities.EntityClass.CommonHistory>>> GetAllPrescriptionPatientHistoryPrevious(int patientId)
        {
            return _prescriptionPatientHistoryQueryRepository.GetAllPrescriptionPatientHistoryPrevious(patientId);
        }

    }
}
