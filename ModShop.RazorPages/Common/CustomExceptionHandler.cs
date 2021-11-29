using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ModShop.Application.Common.Exceptions;
using ModShop.Application.Common.Interfaces;
using ModShop.Common.Util;
using ModShop.Domain.Entities;
using ModShop.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, AppDb appDb)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, appDb);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e, AppDb appDb)
        {

            MethodBase site = e.TargetSite;//Get the methodname from the exception.
            string methodName = site == null ? "" : site.Name;//avoid null ref if it's null.
            methodName = ExtractBracketed(methodName);

            StackTrace stkTrace = new System.Diagnostics.StackTrace(e, true);

            var frame = stkTrace.GetFrame(0);
            string className = ExtractBracketed(frame.GetMethod().ReflectedType.FullName);

            var log = new ErrorLog
            {
                Error = e.GetExceptionMessage(),
                ClassName = site.DeclaringType?.FullName,
                MethodName = site.Name,
                Created = DateTime.Now
            };

            await appDb.ErrorLogs.AddAsync(log);
            await appDb.SaveChangesAsync();
            //switch (exception)
            //{
            //    case ValidationException validationException:
            //        code = HttpStatusCode.BadRequest;
            //        result = JsonConvert.SerializeObject(validationException.Failures);
            //        break;
            //    case BadRequestException badRequestException:
            //        code = HttpStatusCode.BadRequest;
            //        result = badRequestException.Message;
            //        break;
            //    case NotFoundException _:
            //        code = HttpStatusCode.NotFound;
            //        break;
            //}

            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)code;

            //if (result == string.Empty)
            //{
            //    result = JsonConvert.SerializeObject(new { error = exception.Message });
            //}

            //return context.Response.WriteAsync(result);
        }

        private static string ExtractBracketed(string str)
        {
            string s;
            if (str.IndexOf('<') > -1) //using the Regex when the string does not contain <brackets> returns an empty string.
                s = Regex.Match(str, @"\<([^>]*)\>").Groups[1].Value;
            else
                s = str;
            if (s == "")
                return "'Emtpy'"; //for log visibility we want to know if something it's empty.
            else
                return s;

        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
