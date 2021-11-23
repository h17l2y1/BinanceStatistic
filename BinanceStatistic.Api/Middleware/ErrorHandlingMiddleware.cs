using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Models;
using Microsoft.AspNetCore.Http;

namespace BinanceStatistic.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BinanceException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int code = (int)HttpStatusCode.InternalServerError;
            string message = exception.Message;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(message);
        }
    }
}