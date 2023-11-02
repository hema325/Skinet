namespace WebApi.Errors
{
    public class ExceptionResponse: ApiResponse
    {
        public ExceptionResponse(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string? Details { get; }
    }
}
