using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Configurations
{
    public static class ExceptionConfiguration
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, string environment)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    IExceptionHandlerFeature feature = context.Features.Get<IExceptionHandlerFeature>();

                    if (feature != null)
                    {
                        string message = "Request error, contact your system administrator.";

                        if (environment != "Production")
                            message = $"{feature.Error.Message} - {feature.Error.StackTrace}";

                     
                        string applicationResutlt = JsonConvert.SerializeObject(new ApplicationResult<bool>()
                        {
                            Result = false,
                            Message = message
                        });

                        await context.Response.WriteAsync(applicationResutlt);
                    }
                });
            });
        }
    }
}
