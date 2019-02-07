using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spider.Api.Model.ApiResponse;
using Spider.Application.Exceptions;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Spider.Api.Constants;

namespace Spider.Api.Middleware.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            var response = context.Response;
            var spiderException = exception as SpiderException;
            var statusCode = (int) HttpStatusCode.InternalServerError;
            string message = string.Empty;
            string stackTrace = string.Empty;

            if (spiderException != null)
            {
                message = spiderException.Message;
                stackTrace = spiderException.StackTrace;
                statusCode = spiderException.StatusCode;
            }

            response.ContentType = ContentType.Json;
            response.StatusCode = statusCode;
            var errors = new List<ErrorModel>();
            var error = new ErrorModel
            {
                Message = message,
                StatusCode = statusCode,
                StackTrace = stackTrace
            };
            errors.Add(error);

            var apiResponse = new ApiResponseModel
            {
                StatusCode = statusCode,
                Data = null,
                Errors = errors
            };
            var jsonSerializer = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            await response.WriteAsync(JsonConvert.SerializeObject(apiResponse, jsonSerializer));
        }
    }
}