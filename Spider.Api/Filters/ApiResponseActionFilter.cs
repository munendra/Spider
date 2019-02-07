using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Spider.Api.Model.ApiResponse;

namespace Spider.Api.Filters
{
    public class ApiResponseActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null )
            {
                var contextResult = (context.Result as ObjectResult);
                var apiResponse = new ApiResponseModel();
              
                if (contextResult != null)
                {
                    apiResponse = new ApiResponseModel
                    {
                        Data = contextResult?.Value,
                        StatusCode = contextResult.StatusCode.Value
                    };
                    context.Result = new ObjectResult(apiResponse)
                    {
                        StatusCode = apiResponse.StatusCode
                    };
                }
                else
                {
                    var contextObj = context.Result as OkResult;
                    apiResponse.StatusCode = contextObj.StatusCode;
                    context.Result = new OkObjectResult(apiResponse)
                    {
                        StatusCode = contextObj.StatusCode
                    };
                }
               

                
            }
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}