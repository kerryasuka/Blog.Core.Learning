using Blog.Core.AuthHelper.OverWrite;
using Microsoft.AspNetCore.Builder;
using System;

namespace Blog.Core.Middlewares
{
    public static class MiddlewareHelper
    {
        public static IApplicationBuilder UseJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuth>();
        }
    }
}
