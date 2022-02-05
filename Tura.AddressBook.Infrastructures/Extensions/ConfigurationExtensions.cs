using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Mime;
using Tura.AddressBook.Infrastructures.Exceptions;

namespace Tura.AddressBook.Infrastructures.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IApplicationBuilder ConfigureExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            return
                app.Use(async (context, next) =>
                {
                    try
                    {
                        await next.Invoke();
                    }
                    catch (Exception ex)
                    {
                        string result = string.Empty;
                        int statusCode = (int)HttpStatusCode.InternalServerError;

                        //model labels must be lowcase
                        var serializerSettings = new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        };

                        ApiExceptionResponse error = new ApiExceptionResponse();

                        if (ex is ApiExceptionBase apiExceptionBase)
                        {
                            statusCode = apiExceptionBase.StatusCode;
                            error.Errors = apiExceptionBase.Errors;
                            result = JsonConvert.SerializeObject(error, serializerSettings);
                        }
                        else
                        {
                            try
                            {
                                var err = JsonConvert.DeserializeObject<Error>(ex.Message);
                                    error.Errors = new Error[] {
                                    new Error {
                                        Code = err.Code,
                                        Message = err.Message
                                    }};
                                    statusCode = CheckToErrorCode(err.Code);

                                    result = JsonConvert.SerializeObject(error, serializerSettings);
                            }
                            catch (Exception)
                            {
                                error.Errors = new Error[] {
                                new Error {
                                    Code = ErrorCodes.INTERNALSERVERERROR,
                                    Message = ex.Message
                                }};
                                result = JsonConvert.SerializeObject(error, serializerSettings);
                            }
                            
                            
                            
                        }

                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        context.Response.StatusCode = statusCode;
                        await context.Response.WriteAsync(result);
                       
                    }
                });
        }

        private static int CheckToErrorCode(string statusCode)
        {
            switch (statusCode)
            {
                case ErrorCodes.BADREQUEST:
                    return 400;
                case ErrorCodes.UNAUTHORIZED:
                    return 401;
                case ErrorCodes.FORBIDDEN:
                    return 403;
                case ErrorCodes.NOTACCEPTABLE:
                    return 406;
                case ErrorCodes.NOTFOUND:
                    return 404;
                case ErrorCodes.FOUND:
                    return 302;
                case ErrorCodes.TOOMANYREQUEST:
                    return 429;
                default:
                    return 500;
            }
        }
    }
}

