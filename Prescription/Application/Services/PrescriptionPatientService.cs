using ApiCallService.BaseApiCallService;
using ApiCallService.JsonObjectConverService;
using DoctorExpertise.Dtos.RequestDto.DoctorExpertiseDto;
using Entities.EntityClass;
using Google.GenAI;
using Google.GenAI.Types;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prescription.Dtos.RequestDto.DegreeDto;
using Prescription.Dtos.RequestDto.DoctorChamberDto;
using Prescription.Dtos.RequestDto.DoctorDegreeDto;
using Prescription.Dtos.RequestDto.DoctorDto;
using Prescription.Dtos.RequestDto.DoctorExpertise;
using Prescription.Dtos.RequestDto.DoctorScheduleDto;
using Prescription.Dtos.RequestDto.ExaminationsDto;
using Prescription.Dtos.RequestDto.ExpertiseCategoryDto;
using Prescription.Dtos.RequestDto.GeminiDto;
using Prescription.Dtos.RequestDto.Patients;
using Prescription.Dtos.RequestDto.PatientsDto;
using Prescription.Dtos.RequestDto.ScheduleDto;
using Prescription.Dtos.RequestDto.UserDto;
using Prescription.Dtos.ResponseDto.DegreeDto;
using Prescription.Dtos.ResponseDto.DoctorChamberDto;
using Prescription.Dtos.ResponseDto.DoctorDegreeDto;
using Prescription.Dtos.ResponseDto.DoctorDto;
using Prescription.Dtos.ResponseDto.DoctorScheduleDto;
using Prescription.Dtos.ResponseDto.ExaminationsDto;
using Prescription.Dtos.ResponseDto.PatientResponse;
using Prescription.Dtos.ResponseDto.ScheduleDto;
using Prescription.Dtos.ResponseDto.UserDto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utility.Response;

namespace Prescription.Application.Services
{
    public class PrescriptionPatientService
    {
        private readonly IConfiguration _configuration;
        private string _apiBaseURL;
        private readonly IBaseRestClientApiService _baseRestClientApiService;
        private readonly Client _client;
        public PrescriptionPatientService(IConfiguration configuration, IBaseRestClientApiService baseRestClientApiService)
        {
            _configuration = configuration;
            _apiBaseURL = _configuration.GetSection("GeneralSettings:PrescriptionBaseURL").Value;
            _baseRestClientApiService = baseRestClientApiService;
            var apiKey = configuration["Gemini:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("Gemini API key missing");

            // 🔥 DIRECT CALL HERE
            System.Environment.SetEnvironmentVariable("GOOGLE_API_KEY", apiKey);

            _client = new Client();
        }

        public async Task<string> GenerateChatAsync(GeminiChatRequestDto request)
        {
            const int maxRetries = 3;
            int attempt = 0;

            // Build configuration
            var config = new GenerateContentConfig
            {
                Temperature = request.GenerationConfig?.Temperature ?? 0.7,
                MaxOutputTokens = request.GenerationConfig?.MaxOutputTokens ?? 1000
            };

            // Add system instruction if provided
            if (request.SystemInstruction?.Parts != null && request.SystemInstruction.Parts.Any())
            {
                config.SystemInstruction = new Content
                {
                    Parts = request.SystemInstruction.Parts
                        .Select(p => new Part { Text = p.Text })
                        .ToList()
                };
            }

            // Convert frontend contents to Gemini Content format
            var contents = request.Contents.Select(c => new Content
            {
                Role = c.Role,
                Parts = c.Parts.Select(p => new Part { Text = p.Text }).ToList()
            }).ToList();

            while (attempt < maxRetries)
            {
                try
                {
                    // IMPORTANT: Pass contents as IEnumerable, not string
                    var response = await _client.Models.GenerateContentAsync(
                        model: "gemini-2.5-flash",
                        contents: contents, // This should be IEnumerable<Content>
                        config: config
                    );

                    // Extract text from response
                    var text = response?.Candidates?[0]?.Content?.Parts?[0]?.Text;

                    if (string.IsNullOrEmpty(text))
                        throw new Exception("Empty response from AI service");

                    // Normalize output (remove line breaks, normalize spacing)
                    text = text.Replace("\r\n", " ")
                               .Replace("\n", " ")
                               .Replace("\r", " ")
                               .Trim();

                    // Remove multiple spaces
                    while (text.Contains("  "))
                        text = text.Replace("  ", " ");

                    return text;
                }
                catch (Google.GenAI.ServerError ex) when (ex.Message.Contains("overloaded"))
                {
                    attempt++;
                    if (attempt >= maxRetries)
                        throw new InvalidOperationException("AI service overloaded after retries.", ex);

                    // Exponential backoff
                    await Task.Delay((int)Math.Pow(2, attempt - 1) * 1000);
                }
                catch (Exception ex)
                {
                    // Log and rethrow for debugging
                    throw new Exception($"Error calling Gemini API: {ex.Message}", ex);
                }
            }

            throw new Exception("Max retries reached");
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


        public async Task<Response<PatientsResponseDto>> GetByPhoneNo(string PhoneNo)
        {
            var response = new Response<PatientsResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = $"api/2025-02/get-patients-by-phone_no?phoneNo={PhoneNo}";

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
        public async Task<string> CallGemini2Flash(string prompt)
        {
            var apiKey = _configuration["Gemini:ApiKey"];

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}";

            var payload = new
            {
                systemInstruction = new
                {
                    parts = new[]
                    {
                new
                {
                    text = "You are an expert medical assistant for doctors. Audience: Doctors. Assume high medical literacy. Tone: Professional, clinical, concise. Doctor-to-Doctor. Format: Bullet points, exact dosages, interactions. No conversational filler. Constraint: Do not provide general patient advice."
                }
            }
                },
                contents = new[]
                {
            new
            {
                role = "user",
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        },
                generationConfig = new { }
            };

            using var client = new HttpClient();
            var json = System.Text.Json.JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);

            var aiText = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")[0]
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return aiText;
        }
        public async Task<string> GenerateTextAsync(string prompt)
        {
            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
                contents: prompt
            );

            return response.Candidates[0].Content.Parts[0].Text;
        }




    }
}
