using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;

namespace Utility.Response
{
    public static class ResponseHelper
    {
        public static void SetSuccessResponse<T>(Response<T> apiResponse, T? result = default, string? message = null, string? status = null, int? statusCode = null)
        {
            if (apiResponse == null)
            {
                // If apiResponse is null, throw an exception or handle it accordingly.
                throw new ArgumentNullException(nameof(apiResponse), "ApiResponse cannot be null.");
            }
            // If no message is provided, use the default common success message
            if (string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(apiResponse.Message))
            {
                message = "Success : The completion of actions like retrieval, insertion, updating, or deletion of data";
            }
            apiResponse.Result = result;
            apiResponse.Status = status ?? StatusResponseMessage.success;
            apiResponse.Message = message ?? apiResponse.Message;
            apiResponse.StatusCode = statusCode ?? StatusCodes.Status200OK;
            apiResponse.IsSuccess = true;
        }

        public static void SetFailedResponse<T>(Response<T> apiResponse, T? result = default, string? message = null, string? status = null, int? statusCode = null)
        {
            if (apiResponse == null)
            {
                // If apiResponse is null, throw an exception or handle it accordingly.
                throw new ArgumentNullException(nameof(apiResponse), "ApiResponse cannot be null.");
            }

            // If no message is provided, use a default failure message.
            if (string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(apiResponse.Message))
            {
                message = "Failed : The completion of actions like retrieval, insertion, updating, or deletion of data";
            }
            apiResponse.Result = result;
            apiResponse.Status = status ?? StatusResponseMessage.failed;
            apiResponse.Message = message ?? apiResponse.Message;
            apiResponse.StatusCode = statusCode ?? StatusCodes.Status400BadRequest;
            apiResponse.IsSuccess = false;
        }

        public static void SetFailedResponse<T>(PagedWithResponse<T> apiResponse,int? totalCount = null, T? result = default, string? message = null, string? status = null, int? statusCode = null)
        {
            if (apiResponse == null)
            {
                // If apiResponse is null, throw an exception or handle it accordingly.
                throw new ArgumentNullException(nameof(apiResponse), "ApiResponse cannot be null.");
            }

            // If no message is provided, use a default failure message.
            if (string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(apiResponse.Message))
            {
                message = "Failed : The completion of actions like retrieval, insertion, updating, or deletion of data";
            }
            apiResponse.Result = result;
            apiResponse.TotalCount = totalCount ?? 0;
            apiResponse.Status = status ?? StatusResponseMessage.failed;
            apiResponse.Message = message ?? apiResponse.Message;
            apiResponse.StatusCode = statusCode ?? StatusCodes.Status400BadRequest;
            apiResponse.IsSuccess = false;
        }
        public static void SetSuccessResponse<T>(PagedWithResponse<T> apiResponse,int? totalCount=null, T? result = default, string? message = null, string? status = null, int? statusCode = null)
        {
            if (apiResponse == null)
                throw new ArgumentNullException(nameof(apiResponse), "ApiResponse cannot be null.");

            if (string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(apiResponse.Message))
                message = "Success : The completion of actions like retrieval, insertion, updating, or deletion of data";

            apiResponse.Result = result;
            apiResponse.Status = status ?? StatusResponseMessage.success;
            apiResponse.Message = message ?? apiResponse.Message;
            apiResponse.StatusCode = statusCode ?? StatusCodes.Status200OK;
            apiResponse.IsSuccess = true;
            apiResponse.TotalCount = totalCount??0;
        }

    }

}


