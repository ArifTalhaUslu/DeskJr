using DeskJr.Common.Exceptions;
using DeskJr.Service.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;


namespace DeskJr.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;
            var errorDetails = new ErrorResponseDto();

            switch (ex)
            {
                case NotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorDetails = new ErrorResponseDto
                    {
                        StatusCodes = response.StatusCode,
                        Message = "Not Found!",
                        Details = ex.Message
                    };
                    break;
                case BadRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorDetails = new ErrorResponseDto
                    {
                        StatusCodes = response.StatusCode,
                        Message = "Bad Request!",
                        Details = ex.Message
                    };
                    break;
                case UnauthorizedException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorDetails = new ErrorResponseDto
                    {
                        StatusCodes = response.StatusCode,
                        Message = "Unauthorized!",
                        Details = ex.Message
                    };
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorDetails = new ErrorResponseDto
                    {
                        StatusCodes = response.StatusCode,
                        Message = "An unexpected server error occurred.",
                        Details = ex.Message
                    };
                    break;
            }
            var result = JsonSerializer.Serialize(errorDetails);
            return context.Response.WriteAsync(result);
        }
    }
}
