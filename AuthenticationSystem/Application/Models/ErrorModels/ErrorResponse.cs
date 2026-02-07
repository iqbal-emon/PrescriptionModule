namespace AuthenticationSystem.Application.Models.ErrorModels
{
    /// <summary>
    /// Standardized error response model following industry best practices
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Human-readable error message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Machine-readable error code
        /// </summary>
        public string Error { get; set; } = string.Empty;

        /// <summary>
        /// Unique correlation ID for request tracking
        /// </summary>
        public string CorrelationId { get; set; } = string.Empty;

        /// <summary>
        /// Timestamp when the error occurred (UTC)
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Request path that caused the error
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// HTTP method of the request
        /// </summary>
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// Stack trace (only in development)
        /// </summary>
        public string? StackTrace { get; set; }

        /// <summary>
        /// Inner exception message (only in development)
        /// </summary>
        public string? InnerException { get; set; }
    }
}

