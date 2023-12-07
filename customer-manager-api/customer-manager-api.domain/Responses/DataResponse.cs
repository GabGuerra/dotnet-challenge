namespace customer_manager_api.domain.Responses
{
    public class DataResponse<T> where T : class
    {
        public DataResponse()
        {

        }

        public DataResponse(T data)
        {
            Success = true;
            Data = data;
        }

        public DataResponse(bool success, string message, string status)
        {
            Success = success;
            Message = message;
            Status = status;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
        public T Data { get; set; }
    }
}
