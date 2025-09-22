using Microsoft.AspNetCore.Http;
using Notification.Dtos.RequestDto;
using Notification.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace Notification.Application
{
    public class GreenWebSMSService
    {
        public GreenWebSMSService()
        {
            
        }

        public async Task<Response<SmsResponseDto>> SendSmsGreenWeb(SmsSendRequestDto requestModel)
        {
            var response = new Response<SmsResponseDto>();
            string result = "";
            //HttpClient client = new HttpClient();
            WebRequest request = null;
            HttpWebResponse smsResponse = null;

            String to = requestModel.MobileNo; //Recipient Phone Number multiple number must be separated by comma
            String token = requestModel.ApiKey ?? "930215042016799942603064c6b63203838ad38b8f0fc7fcc9cd"; //generate token from the control panel
            String message = requestModel.Sms; //do not use single quotation (') in the message to avoid forbidden result

            string url = "http://api.greenweb.com.bd/api.php?token=" + token + "&to=" + to + "&message=" + message;

            request = WebRequest.Create(url);
            smsResponse = (HttpWebResponse)request.GetResponse();
            if (smsResponse != null)
            {
                var returnResponse = new SmsResponseDto
                {
                    Result = smsResponse.StatusCode.ToString()
                };
                response.Result = returnResponse;
                ResponseHelper.SetSuccessResponse(response, returnResponse, smsResponse.StatusDescription, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            else
            {
                ResponseHelper.SetFailedResponse(response, null, "Failed to send sms", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<SmsResponseDto>> SendSmsGreenWebAsync(SmsSendRequestDto requestModel)
        {
            var response = new Response<SmsResponseDto>();
            string token = requestModel.ApiKey ?? "930215042016799942603064c6b63203838ad38b8f0fc7fcc9cd"; // Token from the control panel
            string url = $"http://api.greenweb.com.bd/api.php?token={token}&to={requestModel.MobileNo}&message={requestModel.Sms}";

            try
            {
                using (var client = new HttpClient())
                {
                    var httpResponse = await client.GetAsync(url);
                    var returnResponse = new SmsResponseDto { Result = httpResponse.StatusCode.ToString() };

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        ResponseHelper.SetSuccessResponse(response, returnResponse, httpResponse.ReasonPhrase, StatusResponseMessage.success, StatusCodes.Status200OK);
                    }
                    else
                    {
                        ResponseHelper.SetFailedResponse(response, returnResponse, "Failed to send SMS", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                ResponseHelper.SetFailedResponse(response, null, ex.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}
