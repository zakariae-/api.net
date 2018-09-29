using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WebUtils.Middlewares
{
    public class LogResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogResponseMiddleware(RequestDelegate next, ILogger<LogResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestBody = await GetRequestBody(context.Request);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log(context, requestBody, ex);
                throw;
            }

            if (context.Response.StatusCode >= 300)
            {
                Log(context, requestBody);
            }
        }

        private void Log(HttpContext context, string body, Exception exception = null)
        {
            var stateList = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("Body", body)
            };

            var url = UriHelper.GetDisplayUrl(context.Request);
            var message = $"Request - {context.Request.Method} {url}";
            if (exception == null) message += $" - StatusCode:{context.Response.StatusCode}";

            _logger.Log(LogLevel.Information, new EventId(), stateList, exception, (dico, ex) => { return message; });
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableRewind();
            var body = request.Body;

            var reader = new StreamReader(request.Body);

            var bodyAsText = await reader.ReadToEndAsync();
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return bodyAsText;

        }
    }

    public static class LogResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogResponseMiddleware>();
        }
    }
}
