using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandBox.ActionFilters
{
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;
            if (result is ViewResult)
            {
                IHeaderDictionary headers = context.HttpContext.Response.Headers;

                if (!headers.ContainsKey("X-Frame-Options")) headers.Add("X-Frame-Options", "SAMEORIGIN");

                var csp = "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline';"
                          + " img-src 'self' data: blob:; font-src 'self' https://fonts.gstatic.com;";
                // once for standards compliant browsers
                if (!headers.ContainsKey("Content-Security-Policy"))
                    headers.Add("Content-Security-Policy", csp);
                // and once again for IE
                if (!headers.ContainsKey("X-Content-Security-Policy"))
                    headers.Add("X-Content-Security-Policy", csp);
            }
        }
    }

}
