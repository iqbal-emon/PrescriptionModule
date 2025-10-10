using Entities.EntityClass;
using Medication.Domain.Repositories.Medication;
using Medication.Dtos.ResponseDto.MedicationDto;
using Medication.Dtos.RquestDto.MedicatonDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace Medication.Application.Services
{
    public class MedicationService
    {
        private readonly IMedicationQueryRepository _medicationQueryRepository;
        private readonly IMedicationCommandRepository _medicationCommandRepository;
        private readonly MapperService _mapperService;
        public MedicationService(IMedicationQueryRepository medicationQueryRepository, IMedicationCommandRepository medicationCommandRepository,
            MapperService mapperService)
        {
            _medicationQueryRepository = medicationQueryRepository;
            _medicationCommandRepository = medicationCommandRepository;
            _mapperService = mapperService;
        }
        public async Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.Medication>>();

            try
            {
                var medication = await _medicationQueryRepository.GetAll();

                if (medication == null)
                {
                    ResponseHelper.SetFailedResponse(response, medication.Result, medication.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, medication.Result, medication.Message, StatusResponseMessage.success, medication.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the medication.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
        public async Task<PagedWithResponse<List<MedicationMostUsedDto>>> GetAllMedicineMostUsed(
      int pageNumber = 1,
      int pageSize = 10,
      string? searchTerm = null,
      string? companyName = null)
        {
            var response = new PagedWithResponse<List<MedicationMostUsedDto>>();

            try
            {
                // ✅ Call repository (which will execute both SPs with filters)
                var pagedData = await _medicationQueryRepository.GetAllMedicineMostUsed(pageNumber, pageSize, searchTerm, companyName);

                if (pagedData.Result == null || pagedData.TotalCount == 0)
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        0,
                        null,
                        "No medications found.",
                        StatusResponseMessage.failed,
                        StatusCodes.Status404NotFound
                    );
                }
                else
                {
                    response.Result = pagedData.Result;
                    response.TotalCount = pagedData.TotalCount;

                    ResponseHelper.SetSuccessResponse(
                        response,
                        response.TotalCount,
                        response.Result,
                        "Medications retrieved successfully.",
                        StatusResponseMessage.success,
                        StatusCodes.Status200OK
                    );
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the medications.";
                ResponseHelper.SetFailedResponse(
                    response,
                    response.TotalCount,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(
                    response,
                    response.TotalCount,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
            }

            return response;
        }





        public async Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.Medication>>();

            try
            {
                var medication = await _medicationQueryRepository.GetBookMarks(doctorId);

                if (medication == null)
                {
                    ResponseHelper.SetFailedResponse(response, medication.Result, medication.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, medication.Result, medication.Message, StatusResponseMessage.success, medication.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the medication.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        
        public async Task<Response<Entities.EntityClass.MedicineEntity.Medication>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.MedicineEntity.Medication>();

            try
            {
                var medication = await _medicationQueryRepository.GetById(id);

                if (medication == null)
                {
                    ResponseHelper.SetFailedResponse(response, medication.Result, medication.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, medication.Result, medication.Message, StatusResponseMessage.success, medication.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the medication.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }


        public async Task<Response<int>> Insert(MedicationInsertRequestDto medication)
        {
            var response = new Response<int>();

            try
            {
                var medicationEntity = await _mapperService.MapSingle<MedicationInsertRequestDto, Entities.EntityClass.MedicineEntity.Medication>(medication);

                var insertResponse = await _medicationCommandRepository.Insert(medicationEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the medication.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the medication.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(MedicationUpdateRequestDto medication)
        {

            var response = new Response<int>();

            try
            {
                var medicationEntity = await _mapperService.MapSingle<MedicationUpdateRequestDto, Entities.EntityClass.MedicineEntity.Medication>(medication);
                medicationEntity.UpdatedAt = DateTime.Now;
                medicationEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _medicationCommandRepository.Update(medicationEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the medication.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);

            }
            catch (Exception ex)
            {
                response.Message = "A database error occurred while updating the medication.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);

            }

            return response;

        }
        public Task<Response<bool>> Delete(int id)
        {
            return _medicationCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetMedicationByName(string medicationName,string uniCode)
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.Medication>>();

            try
            {
                var medication = await _medicationQueryRepository.GetMedicationByName(medicationName,uniCode);

                if (medication == null)
                {
                    ResponseHelper.SetFailedResponse(response, medication.Result, medication.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, medication.Result, medication.Message, StatusResponseMessage.success, medication.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the medication.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

    }
}
