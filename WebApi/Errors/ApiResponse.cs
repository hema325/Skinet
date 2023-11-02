namespace WebApi.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultCodeMessage(statusCode);
        }

        public int StatusCode { get; }
        public string? Message { get; }

        private string? GetDefaultCodeMessage(int statusCode)
            => statusCode switch
            {
                400 => "Provided data is not correct",
                401 => "Unauthorized access",
                404 => "resource wasn't found",
                500 => "Error occurred while processing your request",
                _=> null
            };
        
    }
}
