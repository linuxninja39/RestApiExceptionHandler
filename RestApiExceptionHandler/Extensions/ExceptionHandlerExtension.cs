using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestApiExceptionHandler.Dtos;
using RestApiExceptionHandler.Exceptions;

namespace RestApiExceptionHandler.Extensions
{
    public static class ExceptionHandlerExtension
    {
        public static void UseApiExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                action => action.Run(
                    async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature.Error;
                        if (exception is BaseApiException apiException)
                        {
                            context.Response.StatusCode = apiException.StatusCode;
                        }

                        var errorResponse = new ApiErrorDto
                        {
                            Message = exception.Message
                        };
                        var result = JsonConvert.SerializeObject(errorResponse);
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(result);
                    }
                )
            );
        }

        public static void UseApiExceptionHandler(this IApplicationBuilder app, Action<IApplicationBuilder> action)
        {
            app.UseExceptionHandler(action);
        }
    }
}