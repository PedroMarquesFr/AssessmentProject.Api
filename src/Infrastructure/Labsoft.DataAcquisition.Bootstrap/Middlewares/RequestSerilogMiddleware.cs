using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Bootstrap.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class RequestSerilogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestSerilogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            using (LogContext.PushProperty("UserName", context.User.Identity?.Name ?? "anônimo"))
            {
                return _next.Invoke(context);
            }
        }
    }
}
