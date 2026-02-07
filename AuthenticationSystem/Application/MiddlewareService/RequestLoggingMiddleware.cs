using System.Diagnostics;
using System.Text;

namespace AuthenticationSystem.Application.MiddlewareService
{
    /// <summary>
    /// Middleware to log all API requests and responses, especially errors
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Items["CorrelationId"]?.ToString() ?? Guid.NewGuid().ToString();
            var stopwatch = Stopwatch.StartNew();

            // Capture request details
            var requestPath = context.Request.Path;
            var requestMethod = context.Request.Method;
            var requestQuery = context.Request.QueryString.ToString();
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var user = context.User?.Identity?.Name ?? "Anonymous";

            // Try to read request body (for POST/PUT/PATCH requests)
            string? requestBody = null;
            if (context.Request.ContentLength > 0 && 
                (requestMethod == "POST" || requestMethod == "PUT" || requestMethod == "PATCH"))
            {
                context.Request.EnableBuffering();
                var bodyReader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                requestBody = await bodyReader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            // Log request
            _logger.LogInformation(
                "API Request - CorrelationId: {CorrelationId}, Method: {Method}, Path: {Path}, " +
                "Query: {Query}, User: {User}, IP: {IP}, UserAgent: {UserAgent}, " +
                "RequestBody: {RequestBody}",
                correlationId, requestMethod, requestPath, requestQuery, user, clientIp, userAgent, 
                requestBody ?? "N/A");

            // Capture original response body stream
            var originalBodyStream = context.Response.Body;

            try
            {
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);

                    stopwatch.Stop();

                    // Read response
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
                    responseBody.Seek(0, SeekOrigin.Begin);

                    // Copy response back to original stream
                    await responseBody.CopyToAsync(originalBodyStream);

                    var statusCode = context.Response.StatusCode;

                    // Log response - especially errors (4xx, 5xx)
                    if (statusCode >= 400)
                    {
                        _logger.LogError(
                            "API Error Response - CorrelationId: {CorrelationId}, Method: {Method}, " +
                            "Path: {Path}, StatusCode: {StatusCode}, Duration: {Duration}ms, " +
                            "User: {User}, IP: {IP}, ResponseBody: {ResponseBody}",
                            correlationId, requestMethod, requestPath, statusCode, 
                            stopwatch.ElapsedMilliseconds, user, clientIp, responseBodyText);
                    }
                    else
                    {
                        _logger.LogInformation(
                            "API Response - CorrelationId: {CorrelationId}, Method: {Method}, " +
                            "Path: {Path}, StatusCode: {StatusCode}, Duration: {Duration}ms",
                            correlationId, requestMethod, requestPath, statusCode, 
                            stopwatch.ElapsedMilliseconds);
                    }
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                
                // Log the exception with full context
                _logger.LogError(ex,
                    "API Request Exception - CorrelationId: {CorrelationId}, Method: {Method}, " +
                    "Path: {Path}, Query: {Query}, User: {User}, IP: {IP}, UserAgent: {UserAgent}, " +
                    "Duration: {Duration}ms, RequestBody: {RequestBody}, " +
                    "ExceptionType: {ExceptionType}, ExceptionMessage: {ExceptionMessage}, " +
                    "StackTrace: {StackTrace}, InnerException: {InnerException}",
                    correlationId, requestMethod, requestPath, requestQuery, user, clientIp, 
                    userAgent, stopwatch.ElapsedMilliseconds, requestBody ?? "N/A",
                    ex.GetType().Name, ex.Message, ex.StackTrace,
                    ex.InnerException?.ToString() ?? "None");

                // Re-throw to let GlobalExceptionHandlingMiddleware handle it
                throw;
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}

