using System.Collections.Generic;

namespace Spider.Api.Model.ApiResponse
{
    public class ApiResponseModel
    {
        public ApiResponseModel()
        {
            Errors = new List<ErrorModel>();
        }

        public int StatusCode { get; set; }
        public IList<ErrorModel> Errors { get; set; }
        public object Data { get; set; }
    }
}