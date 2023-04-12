using amaz_commerce_api.Errors;
using Ecommerce.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace amaz_commerce_api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case FluentValidation.ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var errors = validationException.Errors.Select(e => e.ErrorMessage).ToArray();
                        var validationJsons = JsonConvert.SerializeObject(errors);
                        result = JsonConvert.SerializeObject( new CodeErrorException(statusCode, errors, validationJsons));
                        break;
                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, 
                        new string[] { ex.Message }, ex.StackTrace));
                }

                httpContext.Response.StatusCode = statusCode;

                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}
