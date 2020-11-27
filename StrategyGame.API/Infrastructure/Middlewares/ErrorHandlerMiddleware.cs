using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StrategyGame.BLL.Dtos;
using StrategyGame.MODEL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StrategyGame.API.Infrastructure.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlerMiddleware> logger;
        private readonly IWebHostEnvironment env;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger,
            IWebHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline
                await next(httpContext);
            }
            catch (EntityNotFoundException e)
            {
                logger.LogError($"Unhandled {e.GetType()} caught.");

                await WriteAsJsonAsync(
                    httpContext,
                    (int)HttpStatusCode.NotFound,
                    new ErrorDto
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace
                    });
            }
            catch (DomainException e)
            {
                logger.LogError($"Unhandled {e.GetType()} caught.");

                await WriteAsJsonAsync(
                    httpContext,
                    (int)HttpStatusCode.BadRequest,
                    new ErrorDto
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace
                    });
            }
            catch (SqlException e)
            {
                logger.LogError($"Unhandled {e.GetType()} caught.");

                await WriteAsJsonAsync(
                    httpContext,
                    (int)HttpStatusCode.BadRequest,
                    new ErrorDto
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace
                    });
            }
            catch (ValidationAppException e)
            {
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = "application/json";

                var json = SerializeObject(new { e.Errors });
                await httpContext.Response.WriteAsync(json);
            }
            catch (AppException e)
            {
                logger.LogError($"Unhandled {e.GetType()} caught.");

                await WriteAsJsonAsync(
                    httpContext,
                    e.ErrorCode.HasValue
                        ? (int)e.ErrorCode
                        : (int)HttpStatusCode.BadRequest,
                    new ErrorDto
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace
                    });
            }
            catch (DbUpdateConcurrencyException e)
            {
                logger.LogError($"Unhandled {e.GetType()} caught.");

                await WriteAsJsonAsync(
                    httpContext,
                    (int)HttpStatusCode.Conflict,
                    new ErrorDto
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace
                    });
            }
            catch (Exception e)
            {
                logger.LogError($"Unhandled {e.GetType()} caught.");

                await WriteAsJsonAsync(
                    httpContext,
                    (int)HttpStatusCode.InternalServerError,
                    new ErrorDto
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace
                    });
            }
        }

        private Task WriteAsJsonAsync(HttpContext httpContext, int statusCode, ErrorDto payload)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            var json = env.IsDevelopment()
                ? string.IsNullOrWhiteSpace(payload.Message)
                    ? SerializeObject(new { payload.StackTrace })
                    : SerializeObject(payload)
                : string.IsNullOrWhiteSpace(payload.Message)
                    ? ""
                    : SerializeObject(new { payload.Message });

            return httpContext.Response.WriteAsync(json);
        }

        private string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(
                obj,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
