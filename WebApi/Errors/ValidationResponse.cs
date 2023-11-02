namespace WebApi.Errors
{
    public class ValidationResponse : ApiResponse
    {
        public ValidationResponse(IEnumerable<string> errors) : base(StatusCodes.Status400BadRequest) 
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}
