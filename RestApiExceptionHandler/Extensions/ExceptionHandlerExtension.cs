using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RestApiExceptionHandler.Extensions
{
    public static class ExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                action => action.Run(
                    async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature.Error;

                        var result = JsonConvert.SerializeObject(new {error = exception.Message});
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(result);
                    }
                )
            );
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, Action<IApplicationBuilder> action)
        {
            app.UseExceptionHandler(action);
        }
    }
}