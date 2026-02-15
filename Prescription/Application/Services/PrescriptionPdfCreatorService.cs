using ApiCallService.BaseApiCallService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Prescription.Dtos.RequestDto.PatientsDto;
using Prescription.Dtos.ResponseDto.PatientResponse;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;
using Prescription.Dtos.ResponseDto.PrescriptionPdfDto;
using Prescription.Dtos.RequestDto.PrescriptionPdfDto;
using PrescriptionPdf.Dtos.ResponseDto.PdfDto;
using PrescriptionPdf.Dtos.RequestDto.PdfDto;
using Prescription.Dtos.RequestDto.NotificationDto;
using PrescriptionPdf.Dtos.ResponseDto.NotificationDto;

namespace Prescription.Application.Services
{
    public class PrescriptionPdfCreatorService
    {
        private readonly IConfiguration _configuration;
        private string _apiBaseURL;
        private readonly IBaseRestClientApiService _baseRestClientApiService;
        public PrescriptionPdfCreatorService(IConfiguration configuration, IBaseRestClientApiService baseRestClientApiService)
        {
            _configuration = configuration;
            _apiBaseURL = _configuration.GetSection("GeneralSettings:PrescriptionBaseURL").Value;
            _baseRestClientApiService = baseRestClientApiService;
        }
        public async Task<Response<PrescriptionPdfResponseDto>> PdfInsert(PrescriptionPdfRequestDto requestModel)
        {

            var response = new Response<PrescriptionPdfResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-pdf";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, null, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                //int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;
                var results = deSerializedJsonResult["results"];

                var prescriptionPdfApiResponse = new PrescriptionPdfResponseDto()
                {
                    PrescriptionPdfPath = results.ToString(),
                };
                if (results != null)
                {
                    response.Result = prescriptionPdfApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<Response<PdfApiResponseDto>> DbPdfInsert(PdfInsertRequestDto requestModel)
        {

            var response = new Response<PdfApiResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-prescription-pdf";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, null, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                //int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;
                var results = deSerializedJsonResult["results"];

                var PdfApiResponse = new PdfApiResponseDto()
                {
                    PrescriptionPdfId = (int)results,
                };
                if (results != null)
                {
                    response.Result = PdfApiResponse;
                    response.IsSuccess = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<Response<SmsResponseDto>> Notification(SmsSendRequestDto requestModel)
        {

            var response = new Response<SmsResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/send-sms";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, requestModel, null, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                //int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;
                var results = deSerializedJsonResult["results"];

                var smsApiResponse = new SmsResponseDto()
                {
                    Result = results.ToString(),
                };
                if (results != null)
                {
                    response.Result = smsApiResponse;
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
