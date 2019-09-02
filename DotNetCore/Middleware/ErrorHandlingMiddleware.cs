﻿using DotNetCore.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace DotNetCore.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //if (ex is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (ex is MyException) code = HttpStatusCode.BadRequest;

            //var result = JsonConvert.SerializeObject(new { error = ex.Message });

            var result = JsonConvert.SerializeObject(new VMResponse()
            {
                Error = new List<VMError>() {new VMError()
                        {
                            DisplayMessage = "Request Faild",
                            ErrorMessage = exception.Message,
                            RequestURI = context.Request.Path,
                        }
                    },
                Source = MethodBase.GetCurrentMethod().DeclaringType.Name,
                ErrorDetails = "exception.StackTrace",
                DateTime = DateTime.Now.ToString(),
                Status = false
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
