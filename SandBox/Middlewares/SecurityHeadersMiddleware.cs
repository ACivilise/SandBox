using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandBox.Middlewares
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        IHeaderDictionary headers = context.Response.Headers;

        // Web Browser XSS Protection
        if (!headers.ContainsKey("X-Xss-Protection")) headers.Add("X-Xss-Protection", "1");

        // Anti-MIME Sniffing
        if (!headers.ContainsKey("X-Content-Type-Options"))
            headers.Add("X-Content-Type-Options", "nosniff");

        await _next(context);
    }
}

}
