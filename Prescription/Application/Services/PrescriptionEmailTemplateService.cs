using ApiCallService.BaseApiCallService;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Prescription.Dtos.RequestDto.PrescriptionPdfDto;
using Prescription.Dtos.ResponseDto.PrescriptionPdfDto;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace Prescription.Application.Services
{
    public class PrescriptionEmailTemplateService
    {
        private readonly IConfiguration _configuration;
        private string _apiBaseURL;
        private readonly IBaseRestClientApiService _baseRestClientApiService;
        public PrescriptionEmailTemplateService(IConfiguration configuration, IBaseRestClientApiService baseRestClientApiService)
        {
            _configuration = configuration;
            _apiBaseURL = _configuration.GetSection("GeneralSettings:PrescriptionBaseURL").Value;
            _baseRestClientApiService = baseRestClientApiService;
        }

        public async Task<Response<PrescriptionPdfResponseDto>> GetEmailTemplate(Guid Id)
        {

            var response = new Response<PrescriptionPdfResponseDto>();
            try
            {
                var check= new PrescriptionPdfRequestDto()
                {
                    HtmlContent = Id.ToString(),
                };

                var baseUrl = _apiBaseURL;
                var endPoint = "api/2025-02/create-pdf";
                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall(baseUrl, endPoint, Method.Post, check, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                //int results = deSerializedJsonResult["results"]?.Value<int>() ?? 0;
                var results = deSerializedJsonResult["results"];

                var degreeApiResponse = new PrescriptionPdfResponseDto()
                {
                    PrescriptionPdfPath = results[0].ToString(),
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
    }
}
