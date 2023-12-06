namespace customer_manager_api.domain.Responses
{
    public class ApiResponse
    {
        public ApiResponse()
        {

        }

        public ApiResponse(bool success, string message, string status)
        {
            Success = success;
            Message = message;
            Status = status;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
