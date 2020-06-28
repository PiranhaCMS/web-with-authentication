using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Piranha.AspNetCore.Services;

namespace RazorWeb
{
    public class LoginRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext ctx, IApplicationService appService)
        {
            await _next(ctx);

            if (ctx.Response.StatusCode == 401)
            {
                ctx.Response.Redirect($"/login?returnUrl={ appService.Url }");
            }
        }
    }
}
