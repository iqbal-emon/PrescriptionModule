using System.Net;
using System.Text.Json;
using AuthenticationSystem.Application.Models.ErrorModels;
using Microsoft.Data.SqlClient;

namespace AuthenticationSystem.Application.MiddlewareService
{
    /// <summary>
    /// Global exception handling middleware that catches all unhandled exceptions
    /// and returns standardized error responses with proper logging
    /// </summary>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlingMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var correlationId = context.Items["CorrelationId"]?.ToString() ?? Guid.NewGuid().ToString();
            context.Response.ContentType = "application/json";
            
            // Capture request details for logging
            var requestPath = context.Request.Path;
            var requestMethod = context.Request.Method;
            var requestQuery = context.Request.QueryString.ToString();
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var user = context.User?.Identity?.Name ?? "Anonymous";
            
            // Try to capture request body if available
            string? requestBody = null;
            if (context.Request.ContentLength > 0)
            {
                try
                {
                    context.Request.EnableBuffering();
                    var bodyReader = new StreamReader(context.Request.Body, System.Text.Encoding.UTF8, leaveOpen: true);
                    requestBody = await bodyReader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
                catch
                {
                    // Ignore if we can't read the body
                }
            }

            var errorResponse = new ErrorResponse
            {
                CorrelationId = correlationId,
                Timestamp = DateTime.UtcNow,
                Path = requestPath,
                Method = requestMethod
            };

            // Build comprehensive error log message
            var errorLogMessage = $"API Error - CorrelationId: {correlationId}, " +
                $"Method: {requestMethod}, Path: {requestPath}, Query: {requestQuery}, " +
                $"User: {user}, IP: {clientIp}, UserAgent: {userAgent}, " +
                $"ExceptionType: {exception.GetType().Name}, ExceptionMessage: {exception.Message}";

            switch (exception)
            {
                case UnauthorizedAccessException:
                    errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.Message = "Unauthorized access. Please check your credentials.";
                    errorResponse.Error = "UNAUTHORIZED_ACCESS";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    _logger.LogWarning(exception, 
                        "{ErrorMessage}, RequestBody: {RequestBody}, StackTrace: {StackTrace}",
                        errorLogMessage, requestBody ?? "N/A", exception.StackTrace ?? "N/A");
                    break;

                case ArgumentException argEx:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = argEx.Message;
                    errorResponse.Error = "INVALID_ARGUMENT";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    _logger.LogWarning(exception, 
                        "{ErrorMessage}, RequestBody: {RequestBody}, StackTrace: {StackTrace}",
                        errorLogMessage, requestBody ?? "N/A", exception.StackTrace ?? "N/A");
                    break;

                case KeyNotFoundException:
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = "The requested resource was not found.";
                    errorResponse.Error = "RESOURCE_NOT_FOUND";
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    _logger.LogWarning(exception, 
                        "{ErrorMessage}, RequestBody: {RequestBody}, StackTrace: {StackTrace}",
                        errorLogMessage, requestBody ?? "N/A", exception.StackTrace ?? "N/A");
                    break;

                case InvalidOperationException invOpEx:
                    // DI/service resolution failures are server config errors, not client errors
                    var isServiceResolution = invOpEx.Message.Contains("Unable to resolve service", StringComparison.OrdinalIgnoreCase);
                    if (isServiceResolution)
                    {
                        errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.Message = _environment.IsDevelopment()
                            ? invOpEx.Message
                            : "A required service is not configured. Please contact support.";
                        errorResponse.Error = "SERVICE_RESOLUTION_FAILED";
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        _logger.LogError(exception,
                            "{ErrorMessage}, RequestBody: {RequestBody}, StackTrace: {StackTrace}",
                            errorLogMessage, requestBody ?? "N/A", exception.StackTrace ?? "N/A");
                    }
                    else
                    {
                        errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Message = invOpEx.Message;
                        errorResponse.Error = "INVALID_OPERATION";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogWarning(exception,
                            "{ErrorMessage}, RequestBody: {RequestBody}, StackTrace: {StackTrace}",
                            errorLogMessage, requestBody ?? "N/A", exception.StackTrace ?? "N/A");
                    }
                    break;

                case SqlException sqlEx:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "A database error occurred. Please contact support if this persists.";
                    errorResponse.Error = "DATABASE_ERROR";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    
                    // Get first error details if available
                    var firstError = sqlEx.Errors.Count > 0 ? sqlEx.Errors[0] : null;
                    var databaseName = firstError?.Database ?? "N/A";
                    var serverName = firstError?.Server ?? "N/A";
                    var procedureName = firstError?.Procedure ?? "N/A";
                    var lineNumber = firstError?.LineNumber ?? sqlEx.LineNumber;

                    // Log SQL errors with full details
                    _logger.LogError(exception,
                        "{ErrorMessage}, SQL Error Number: {ErrorNumber}, SQL State: {State}, " +
                        "SQL Server: {Server}, Database: {Database}, Procedure: {Procedure}, " +
                        "LineNumber: {LineNumber}, RequestBody: {RequestBody}, StackTrace: {StackTrace}",
                        errorLogMessage, sqlEx.Number, sqlEx.State, serverName,
                        databaseName, procedureName, lineNumber,
                        requestBody ?? "N/A", exception.StackTrace ?? "N/A");
                    break;

                case TimeoutException timeoutEx:
                    errorResponse.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    errorResponse.Message = "The request timed out. Please try again.";
                    errorResponse.Error = "REQUEST_TIMEOUT";
                    context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    _logger.LogWarning(exception, 
                        "{ErrorMessage}, RequestBody: {RequestBody}, StackTrace: {StackTrace}",
                        errorLogMessage, requestBody ?? "N/A", exception.StackTrace ?? "N/A");
                    break;

                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = _environment.IsDevelopment() 
                        ? exception.Message 
                        : "An error occurred while processing your request. Please try again later.";
                    errorResponse.Error = "INTERNAL_SERVER_ERROR";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    
                    // Log full exception details with all context - this is critical for debugging
                    _logger.LogError(exception, 
                        "{ErrorMessage}, RequestBody: {RequestBody}, " +
                        "InnerException: {InnerException}, StackTrace: {StackTrace}",
                        errorLogMessage, 
                        requestBody ?? "N/A",
                        exception.InnerException?.ToString() ?? "None",
                        exception.StackTrace ?? "N/A");
                    break;
            }

            // Include stack trace in development
            if (_environment.IsDevelopment())
            {
                errorResponse.StackTrace = exception.StackTrace;
                errorResponse.InnerException = exception.InnerException?.Message;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = _environment.IsDevelopment()
            };

            var json = JsonSerializer.Serialize(errorResponse, options);
            await context.Response.WriteAsync(json);
        }
    }
}

