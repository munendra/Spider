namespace Spider.Api.Model.ApiResponse
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public object StackTrace { get; set; }
    }
}