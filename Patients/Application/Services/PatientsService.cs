using Entities.EntityClass.PatientEntity;
using Microsoft.AspNetCore.Http;
using PatienFolowUp.Domain.Repositories.Patients;
using PatienFolowUp.Dtos.RequestDto.Patients;
using PatienFolowUp.Dtos.RequestDto.PatientsDto;
using PatienFolowUp.Dtos.ResponseDto.Patients;
using PatienFolowUp.Utility;
using Patients.Dtos.ResponseDto.PatientsDto;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace PatienFolowUp.Application.Services
{
    public class PatientsService
    {
        private readonly IPatientsQueryRepository _patientsQueryRepository;
        private readonly IPatientsCommandRepository _patientsCommandRepository;
        private readonly MapperService _mapperService;
        public PatientsService(IPatientsQueryRepository patientsQueryRepository, IPatientsCommandRepository patientsCommandRepository,
            MapperService mapperService)
        {
            _patientsQueryRepository = patientsQueryRepository;
            _patientsCommandRepository = patientsCommandRepository;
            _mapperService = mapperService;
        }
        public async Task<Response<List<PatientDataDto>>> GetAllPatients(int pageNumber = 1, int pageSize = 10, string searchTerm = "",int? doctorId=null)
        {
            var response = new Response<List<PatientDataDto>>();

            var patientsResponse = await _patientsQueryRepository.GetAll(pageNumber, pageSize, searchTerm, doctorId);

            if (!patientsResponse.IsSuccess || patientsResponse.Result == null)
            {
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    patientsResponse.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
                return response;
            }

            // Map Patient -> DTO
            var mappedPatients =  patientsResponse.Result;

            response.Result = mappedPatients;
            ResponseHelper.SetSuccessResponse(
                response,
                mappedPatients,
                PatientsResponseMessage.common_get_all_success,
                StatusResponseMessage.success,
                StatusCodes.Status200OK
            );

            return response;
        }

        public async Task<Response<Patient>> GetById(int id)
        {
            var response = new Response<Patient>();

            try
            {
                var patients = await _patientsQueryRepository.GetById(id);

                if (patients == null)
                {
                    ResponseHelper.SetFailedResponse(response, patients.Result, patients.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message, StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the patients.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Patient>> GetByRoleAndReferenceId(int referenceId)
        {
            var response = new Response<Patient>();

            try
            {
                var patients = await _patientsQueryRepository.GetByRoleAndReferenceId(referenceId);

                if (patients == null)
                {
                    ResponseHelper.SetFailedResponse(response, patients.Result, patients.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message, StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the patients.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        


        public async Task<Response<int>> Insert(PatientsInsertRequestDto patients)
        {
            var response = new Response<int>();

            try
            {
                var patientsEntity = await _mapperService.MapSingle<PatientsInsertRequestDto, Patient>(patients);

                var insertResponse = await _patientsCommandRepository.Insert(patientsEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the patients.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the patients.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(PatientsUpdateRequestDto patients)
        {

            var response = new Response<int>();

            try
            {
                var patientsEntity = await _mapperService.MapSingle<PatientsUpdateRequestDto, Patient>(patients);
                patientsEntity.UpdatedAt = DateTime.Now;
                patientsEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _patientsCommandRepository.Update(patientsEntity);
                response = updatedResponse;

            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the patients.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);

            }
            catch (Exception ex)
            {
                response.Message = "A database error occurred while updating the patients.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);

            }

            return response;

        }
        public Task<Response<bool>> Delete(int id)
        {
            return _patientsCommandRepository.Delete(id);
        }

    }
}
