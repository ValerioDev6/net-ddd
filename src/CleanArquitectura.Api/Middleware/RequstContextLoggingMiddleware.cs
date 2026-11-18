using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql.Replication;
using Serilog.Context;

namespace CleanArquitectura.Api.Middleware
{
    public class RequstContextLoggingMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        private readonly RequestDelegate _next;

        public RequstContextLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
            {
                return _next(context);

            }

        }


        private static string GetCorrelationId(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue(
                CorrelationIdHeaderName,
                out var correlationId
            );

            return correlationId.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }



    }


}
