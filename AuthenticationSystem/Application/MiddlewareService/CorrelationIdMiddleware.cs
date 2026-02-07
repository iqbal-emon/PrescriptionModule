namespace AuthenticationSystem.Application.MiddlewareService
{
    /// <summary>
    /// Middleware to generate and track correlation IDs for request tracing
    /// </summary>
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Try to get correlation ID from request header, otherwise generate a new one
            var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault() 
                ?? Guid.NewGuid().ToString();

            // Store in HttpContext.Items for use throughout the request pipeline
            context.Items["CorrelationId"] = correlationId;

            // Add to response headers so clients can track their requests
            context.Response.Headers[CorrelationIdHeaderName] = correlationId;

            // Add to log context for automatic inclusion in all logs
            using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(context);
            }
        }
    }
}

