using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandBox.Middlewares
{
    public class NoCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public NoCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                var typedHeaders = context.Response.GetTypedHeaders();

                if (typedHeaders.CacheControl == null || !typedHeaders.CacheControl.MaxAge.HasValue)
                {
                    // Cache-control
                    typedHeaders.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                    // Pragma (pour les navigateurs qui ne supportent que HTTP/1.0)
                    typedHeaders.Headers.Append("Pragma", "no-cache");
                    typedHeaders.Headers.Append("Expires", "0");
                }

                return Task.CompletedTask;
            });

            await _next.Invoke(context);
        }
    }

}
