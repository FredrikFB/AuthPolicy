using System.Net;

namespace AuthenticationAppUser.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public ApiResponse(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            IsSuccess = false;
            ErrorMessages = new List<string> { errorMessage };
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object? Result { get; set; }
    }
}
