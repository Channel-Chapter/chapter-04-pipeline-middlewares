using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;
using System.Text.Json;

namespace MiddlewareHandsOn.Api.ExceptionProviders
{
    public static class CustomExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseCustomExceptionHandlerMiddleware(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app.UseExceptionHandler(
                new ExceptionHandlerOptions
                {
                    ExceptionHandler = async context =>
                    {
                        var _exceptionThrown = context.Features.Get<IExceptionHandlerPathFeature>();
                        string _customErrorCode = null;

                        Debug.WriteLine($"Exceção: {_exceptionThrown.Error.Message}");

                        if (_exceptionThrown.Error is ApiCustomException _customException)
                            _customErrorCode = _customException.ErrorCode;

                        await context.Response.WriteAsync(
                                                    ToJson(
                                                        new
                                                        {
                                                            Code = _customErrorCode,
                                                            HttpStatusCode = context.Response.StatusCode,
                                                            Message = _exceptionThrown.Error.Message,
                                                            Details = env.IsDevelopment()
                                                                ? ToJson(_exceptionThrown.Error)
                                                                : null
                                                        }));

                    }
                });

            return app;
        }

        private static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
