using ApiCallService.BaseApiCallService;
using ApiCallService.JsonObjectConverService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Prescription.Dtos.RequestDto.DegreeDto;
using Prescription.Dtos.RequestDto.DoctorDegreeDto;
using Prescription.Dtos.RequestDto.DoctorDto;
using Prescription.Dtos.RequestDto.UserDto;
using Prescription.Dtos.ResponseDto.DegreeDto;
using Prescription.Dtos.ResponseDto.DoctorDegreeDto;
using Prescription.Dtos.ResponseDto.DoctorDto;
using Prescription.Dtos.ResponseDto.PatientResponse;
using Prescription.Dtos.ResponseDto.UserDto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;
using Prescription.Dtos.RequestDto.DoctorChamberDto;
using Prescription.Dtos.RequestDto.PatientsDto;
using Prescription.Dtos.ResponseDto.DoctorChamberDto;
using Prescription.Dtos.RequestDto.ExpertiseCategoryDto;
using Prescription.Dtos.ResponseDto.ExaminationsDto;
using Prescription.Dtos.RequestDto.ExaminationsDto;
using Prescription.Dtos.RequestDto.Patients;
using DoctorExpertise.Dtos.RequestDto.DoctorExpertiseDto;
using Prescription.Dtos.RequestDto.DoctorExpertise;
using Entities.EntityClass;
using Prescription.Dtos.RequestDto.ScheduleDto;
using Prescription.Dtos.ResponseDto.ScheduleDto;
using Prescription.Dtos.ResponseDto.DoctorScheduleDto;
using Prescription.Dtos.RequestDto.DoctorScheduleDto;

namespace Prescription.Application.Services
{
    public class PrescriptionPatientService
    {
        private readonly IConfiguration _configuration;
        private string _apiBaseURL;
        private readonly IBaseRestClientApiService _baseRestClientApiService;
        public PrescriptionPatientService(IConfiguration configuration, IBaseRestClientApiService baseRestClientApiService)
        {
            _configuration = configuration;
            _apiBaseURL = _configuration.GetSection("GeneralSettings:PrescriptionBaseURL").Value;
            _baseRestClientApiService = baseRestClientApiService;
        }
        public async Task<Response<PatientsResponseDto>> PatientInsert(PatientsInsertRequestDto requestModel)
        {

            var response = new Response<PatientsResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-patients";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var degreeApiResponse = new PatientsResponseDto()
                {
                    PatientID=results,
                };
                if (results != null)
                {
                    response.Result = degreeApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<PatientsResponseDto>> PatientUpdate(PatientsUpdateRequestDto requestModel)
        {
            var response = new Response<PatientsResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/update-patients";

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;

                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Put, requestModel, token, 3, 1000);

                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctorDetailsApiResponse = new PatientsResponseDto
                {
                    PatientID = results
                };

                if (results != 0)  // Ensuring the result is valid
                {
                    response.Result = doctorDetailsApiResponse;
                    response.IsSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Response<PatientsResponseDto>> GetByPatientId(int? userId)
        {
            var response = new Response<PatientsResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = $"api/2025-02/get-patient-by-user-id?patientUserId={userId}";

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                var userData = deSerializedJsonResult["results"]?.ToObject<PatientsResponseDto>();

                if (userData != null)
                {
                    response.Result = userData;
                    response.IsSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Response<DegreeApiResponseDto>> DegreeInsert(DegreeInsertRequestDto requestModel)
        {

            var response = new Response<DegreeApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-degree";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                // var deSerializedResults = JsonHelper.DeserializeJsonToSignle<DegreeApiResponseDto>(responseJson.Content);/
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var degreeApiResponse = new DegreeApiResponseDto()
                {
                    DegreeID = results,
                };
                if (results != null)
                {
                    response.Result = degreeApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<ScheduleApiResponseDto>> ScheduleInsert(ScheduleInsertRequestDto requestModel)
        {

            var response = new Response<ScheduleApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-schedule";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
               // var deSerializedResults = JsonHelper.DeserializeJsonToSignle<DegreeApiResponseDto>(responseJson.Content);/
               var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var degreeApiResponse = new ScheduleApiResponseDto()
                { 
                    ScheduleID = results,
                };
                if (results != null)
                {
                    response.Result = degreeApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<DoctorScheduleApiResponseDto>> DoctorScheduleInsert(DoctorScheduleInsertRequestDto requestModel)
        {

            var response = new Response<DoctorScheduleApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-doctor-schedule";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                // var deSerializedResults = JsonHelper.DeserializeJsonToSignle<DegreeApiResponseDto>(responseJson.Content);/
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var degreeApiResponse = new DoctorScheduleApiResponseDto()
                {
                    DoctorScheduleID = results,
                };
                if (results != null)
                {
                    response.Result = degreeApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<DoctorDegreeApiResponseDto>> DoctorDegreeInsert(DoctorDegreeInsertRequestDto requestModel)
        {

            var response = new Response<DoctorDegreeApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-doctor-degree";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctordegreeApiResponse = new DoctorDegreeApiResponseDto()
                {
                    DegreeID = results,
                };

                if (results != null)
                {
                    response.Result = doctordegreeApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<DoctorChamberApiResponseDto>> InsertDoctorChamber(DoctorChamberInsertRequestDto requestModel)
        {

            var response = new Response<DoctorChamberApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-doctor-chamber";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctordegreeApiResponse = new DoctorChamberApiResponseDto()
                {
                    DoctorID = results,
                };

                if (results != null)
                {
                    response.Result = doctordegreeApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<ExpertiseCategoryApiResponseDto>> ExpertiseCategoryInsert(ExpertiseCategoryInsertRequestDto requestModel)
        {

            var response = new Response<ExpertiseCategoryApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-expertise-category";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctorExpertiseApiResponse = new ExpertiseCategoryApiResponseDto()
                {
                    ExpertiseID = results,
                };

                if (results != null)
                {
                    response.Result = doctorExpertiseApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<DoctorExpertiseApiResponseDto>> DoctorExpertiseInsert(DoctorExpertiseInsertRequestDto requestModel)
        {

            var response = new Response<DoctorExpertiseApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-doctor-expertise";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctorExpertiseApiResponse = new DoctorExpertiseApiResponseDto()
                {
                    ExpertiseID = results,
                };

                if (results != null)
                {
                    response.Result = doctorExpertiseApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<ExaminationsApiResponseDto>> ExaminationInsert(ExaminationsInsertRequestDto requestModel)
        {

            var response = new Response<ExaminationsApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-examinations";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctorExminationApiResponse = new ExaminationsApiResponseDto()
                {
                    ExaminationID = results,
                };

                if (results != null)
                {
                    response.Result = doctorExminationApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<DoctorApiResponseDto>> DoctorInsert(DoctorInsertRequestDto requestModel)
        {

            var response = new Response<DoctorApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-doctor";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctorDetailsApiResponse = new DoctorApiResponseDto
                {
                    DoctorID = results
                };

                if (results != null)
                {
                    response.Result = doctorDetailsApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Response<UserApiResponseDto>> UserUpdate(UserUpdateRequestDto requestModel)
        {
            var response = new Response<UserApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/update-user";

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;

                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Put, requestModel, token, 3, 1000);

                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctorDetailsApiResponse = new UserApiResponseDto
                {
                    UserId = results
                };

                if (results != 0)  // Ensuring the result is valid
                {
                    response.Result = doctorDetailsApiResponse;
                    response.IsSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Response<DoctorApiResponseDto>> DoctorUpdate(DoctorUpdateRequestDto requestModel)
        {
            var response = new Response<DoctorApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/update-doctor";
                requestModel.DoctorReferenceID = requestModel.UserID;

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;

                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Put, requestModel, token, 3, 1000);

                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctorDetailsApiResponse = new DoctorApiResponseDto
                {
                    DoctorID = results
                };

                if (results != 0)  // Ensuring the result is valid
                {
                    response.Result = doctorDetailsApiResponse;
                    response.IsSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Response<UserApiResponseDto>> UserGetByReferenceIdAndRole(int? referenceId,string role,int? tenantId)
        {
            var response = new Response<UserApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = $"api/2025-02/get-user-by-referenceid-role?referenceId={referenceId}&role={role}&tenantId={tenantId}";

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                var userData = deSerializedJsonResult["results"]?.ToObject<UserApiResponseDto>();

                if (userData != null)
                {
                    response.Result = userData;
                    response.IsSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Response<DoctorApiResponseDto>> GetByDoctorid(int? userId)
        {
            var response = new Response<DoctorApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = $"api/2025-02/get-doctor-by-user-id?doctorUserId={userId}";

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                var userData = deSerializedJsonResult["results"]?.ToObject<DoctorApiResponseDto>();

                if (userData != null)
                {
                    response.Result = userData;
                    response.IsSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Response<UserApiResponseDto>> UserInsert(UserInsertRequestDto requestModel)
        {

            var response = new Response<UserApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-user";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctordegreeApiResponse = new UserApiResponseDto
                {
                    UserId = results,
                };

                if (results != null)
                {
                    response.Result = doctordegreeApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }


        }


        public async Task<Response<DoctorChamberApiResponseDto>> DoctorChamberInsert(DoctorChamberInsertRequestDto requestModel)
        {

            var response = new Response<DoctorChamberApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-doctor-chamber";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;

                var doctorDetailsApiResponse = new DoctorChamberApiResponseDto
                {
                    ChamberID = results
                };

                if (results != null)
                {
                    response.Result = doctorDetailsApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }







    }
}
