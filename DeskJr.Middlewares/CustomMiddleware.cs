using DeskJr.Common.Exceptions;
using DeskJr.Service.Abstract;
using DeskJr.Service.Dtos;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DeskJr.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var responseBody = new MemoryStream())
            {
                string requestBody = "";
                var originalBodyStream = context.Response.Body;
                context.Response.Body = responseBody;

                try
                {
                    requestBody = await ReadRequestBodyAsync(context.Request);

                    await _next(context);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex);
                }
                finally
                {
                    await LogAndReturnResponseAsync(context, requestBody, responseBody, originalBodyStream);
                }
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
        
        private async Task LogAndReturnResponseAsync(HttpContext context, string requestBody, MemoryStream responseBody, Stream originalBodyStream)
        {
            var responseBodyText = await ReadStreamAsync(responseBody);
            var maskedResponseBody = MaskSensitiveData(responseBodyText);
            var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                     ?? context.Connection.RemoteIpAddress?.ToString();

            var logDetail = new
            {
                RequestMethod = context.Request.Method,
                RequestUrl = context.Request.Path,
                RequestBody = requestBody,
                StatusCode = context.Response.StatusCode,
                ResponseBody = maskedResponseBody,
                IPAddress = ipAddress,
            };

            string logMessage = context.Response.StatusCode >= 400 ? "Request failed" : "Request succeeded";

            Log.Information("Request {RequestMethod} {RequestUrl} with {RequestBody} responded with {StatusCode}. Response: {ResponseBody}. LogMessage: {LogMessage} IpAdress: {IpAdress}",
                logDetail.RequestMethod, logDetail.RequestUrl, logDetail.RequestBody, logDetail.StatusCode, logDetail.ResponseBody, logMessage, logDetail.IPAddress);

            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static string MaskSensitiveData(string responseBody)
        {
            var tokenPattern = @"token"":""[^""]+";
            return Regex.Replace(responseBody, tokenPattern, match =>
                match.Value.Substring(0, match.Value.IndexOf(':') + 1) + "[MASKED]");
        }

        private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();
            using (var reader = new StreamReader(request.Body, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return body;
            }
        }

        private static async Task<string> ReadStreamAsync(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(stream).ReadToEndAsync();
            stream.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}