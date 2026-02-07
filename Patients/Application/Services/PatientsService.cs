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
        public async Task<PagedWithResponse<List<PatientDataDto>>> GetAllPatients(int pageNumber = 1, int pageSize = 10, string searchTerm = "", int? doctorId = null, string followupdate ="")
        {
            var response = new PagedWithResponse<List<PatientDataDto>>();

            var patientsResponse = await _patientsQueryRepository.GetAll(pageNumber, pageSize, searchTerm, doctorId, followupdate);

           if (!patientsResponse.IsSuccess || patientsResponse.Result == null)
{
    ResponseHelper.SetFailedResponse(
        response,
        patientsResponse.TotalCount,
        null,
        patientsResponse.Message,
        StatusResponseMessage.failed,
        StatusCodes.Status400BadRequest
    );
    return response;
}


            // Map Patient -> DTO
            var mappedPatients = patientsResponse.Result;
            var totalCount = patientsResponse.TotalCount;

            response.Result = mappedPatients;

            ResponseHelper.SetSuccessResponse(
                response,               // ✅ apiResponse
                totalCount,             // ✅ totalCount
                mappedPatients,         // ✅ result
                PatientsResponseMessage.common_get_all_success,
                StatusResponseMessage.success,
                StatusCodes.Status200OK
            );


            return response;
        }
        public async Task<PagedWithResponse<FollowUpResponseDto>> GetFollowUpPatients(
     int? doctorId,
     string startDate,
     string endDate)
        {
            var response = new PagedWithResponse<FollowUpResponseDto>();

            // Repository থেকে data নিন
            var patientsResponse = await _patientsQueryRepository.GetFollowUpPatients(
                doctorId,
                startDate,
                endDate);

            if (!patientsResponse.IsSuccess || patientsResponse.Result == null || patientsResponse.Result.Count == 0)
            {
                ResponseHelper.SetFailedResponse(
                    response,
                    0,
                    new FollowUpResponseDto { Followups = new List<FollowUpGroupDto>() },
                    patientsResponse.Message ?? "No follow-up patients found.",
                    StatusResponseMessage.failed,
                    StatusCodes.Status404NotFound
                );
                return response;
            }

            // Group by FollowUpDate করুন
            var groupedData = patientsResponse.Result
                .Where(p => !string.IsNullOrEmpty(p.FollowupDate))  // Null or empty follow-up dates বাদ দিন
                .GroupBy(p => DateTime.Parse(p.FollowupDate).Date)
                .Select(g => new FollowUpGroupDto
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Patients = g.Select(p => new FollowUpPatientDto
                    {
                        PatientName = $"{p.FirstName} {p.LastName}".Trim(),
                        PatientId = p.PatientReferenceID.ToString(),
                        Age=p.PatientAge,
                        Gender=p.Gender,
                        AppointmentTime = "N/A",  // যদি appointment time না থাকে
                        ContactNumber = p.PhoneNumber ?? "N/A"
                    }).ToList()
                })
                .OrderBy(g => g.Date)  // Date অনুযায়ী sort করুন
                .ToList();

            var result = new FollowUpResponseDto
            {
                Followups = groupedData
            };

            ResponseHelper.SetSuccessResponse(
                response,
                patientsResponse.TotalCount,
                result,
                PatientsResponseMessage.common_get_all_success,
                StatusResponseMessage.success,
                StatusCodes.Status200OK
            );

            return response;
        }
        public async Task<Response<PatientsApiResponseDto>> GetById(int id)
        {
            var response = new Response<PatientsApiResponseDto>();

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
        public async Task<Response<PatientsApiResponseDto>> GetByPhoneNo(string phoneNo)
        {
            var response = new Response<PatientsApiResponseDto>();

            try
            {
                var patients = await _patientsQueryRepository.GetByPhoneNo(phoneNo); 

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
        public async Task<Response<List<PatientAgeDistributionResponseDto>>> GetAgeDistribution()
        {
            return await _patientsQueryRepository.GetAgeDistribution();
        }


        public Task<Response<bool>> Delete(int id)
        {
            return _patientsCommandRepository.Delete(id);
        }

        public async Task<Response<PatientsApiResponseDto>> GetByPhoneAndCode(string pCode, string pPhone)
        {
            var response = new Response<PatientsApiResponseDto>();

            try
            {
                var patients = await _patientsQueryRepository.GetByPhoneAndCode(pCode, pPhone);

                if (patients == null || patients.Result == null)
                {
                    ResponseHelper.SetFailedResponse(response, null, "Patient not found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message, StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the patient.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<PatientsApiResponseDto>> GetByUserName(string userName)
        {
            var response = new Response<PatientsApiResponseDto>();

            try
            {
                var patients = await _patientsQueryRepository.GetByUserName(userName);

                if (patients == null || patients.Result == null)
                {
                    ResponseHelper.SetFailedResponse(response, null, "Patient not found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message, StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the patient.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<PatientsApiResponseDto>> GetByUserId(int userId)
        {
            var response = new Response<PatientsApiResponseDto>();

            try
            {
                var patients = await _patientsQueryRepository.GetByUserId(userId);

                if (patients == null || patients.Result == null)
                {
                    ResponseHelper.SetFailedResponse(response, null, "Patient not found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message, StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the patient.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<PatientsApiResponseDto>>> GetAllPatients()
        {
            var response = new Response<List<PatientsApiResponseDto>>();

            try
            {
                var patients = await _patientsQueryRepository.GetAllPatients();

                if (patients == null || patients.Result == null || !patients.Result.Any())
                {
                    ResponseHelper.SetFailedResponse(response, new List<PatientsApiResponseDto>(), "No patients found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message ?? "Patients retrieved successfully", StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving patients.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<PatientsApiResponseDto>>> GetPatientListByUserProfileId(int profileId, string role)
        {
            var response = new Response<List<PatientsApiResponseDto>>();

            try
            {
                var patients = await _patientsQueryRepository.GetPatientListByUserProfileId(profileId, role);

                if (patients == null || patients.Result == null || !patients.Result.Any())
                {
                    ResponseHelper.SetFailedResponse(response, new List<PatientsApiResponseDto>(), "No patients found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message ?? "Patients retrieved successfully", StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving patients.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<PatientsApiResponseDto>>> GetPatientListBySearchUserProfileId(int profileId, string role, string name)
        {
            var response = new Response<List<PatientsApiResponseDto>>();

            try
            {
                var patients = await _patientsQueryRepository.GetPatientListBySearchUserProfileId(profileId, role, name);

                if (patients == null || patients.Result == null || !patients.Result.Any())
                {
                    ResponseHelper.SetFailedResponse(response, new List<PatientsApiResponseDto>(), "No patients found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message ?? "Patients retrieved successfully", StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving patients.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<PatientsApiResponseDto>>> GetPatientListFilter(string searchTerm = "")
        {
            var response = new Response<List<PatientsApiResponseDto>>();

            try
            {
                var patients = await _patientsQueryRepository.GetPatientListFilter(searchTerm);

                if (patients == null || patients.Result == null || !patients.Result.Any())
                {
                    ResponseHelper.SetFailedResponse(response, new List<PatientsApiResponseDto>(), "No patients found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message ?? "Patients retrieved successfully", StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving patients.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<PatientsApiResponseDto>>> GetPatientListByAgentMaster(int masterId)
        {
            var response = new Response<List<PatientsApiResponseDto>>();

            try
            {
                var patients = await _patientsQueryRepository.GetPatientListByAgentMaster(masterId);

                if (patients == null || patients.Result == null || !patients.Result.Any())
                {
                    ResponseHelper.SetFailedResponse(response, new List<PatientsApiResponseDto>(), "No patients found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message ?? "Patients retrieved successfully", StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving patients by agent master.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<PatientsApiResponseDto>>> GetPatientListByAgentSupervisor(int supervisorId)
        {
            var response = new Response<List<PatientsApiResponseDto>>();

            try
            {
                var patients = await _patientsQueryRepository.GetPatientListByAgentSupervisor(supervisorId);

                if (patients == null || patients.Result == null || !patients.Result.Any())
                {
                    ResponseHelper.SetFailedResponse(response, new List<PatientsApiResponseDto>(), "No patients found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, patients.Result, patients.Message ?? "Patients retrieved successfully", StatusResponseMessage.success, patients.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving patients by agent supervisor.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

    }
}
