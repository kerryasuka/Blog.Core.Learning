using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.AuthHelper.OverWrite
{
    /// <summary>
    /// 中间件
    /// 原作为自定义授权的中间件
    /// 现作为检查header token使用
    /// </summary>
    public class JwtTokenAuth
    {
        private readonly RequestDelegate _next;

        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }

        private void PreProceed(HttpContext next)
        {
            //Console.WriteLine($"{DateTimeOffset.Now} middleware invoke prepoceed.");
            //...
        }

        private void PostProceed(HttpContext next)
        {
            //Console.WriteLine($"{DateTimeOffset.Now} middleware invoke postproceed.");
            //...
        }

        public Task Invoke(HttpContext httpContext)
        {
            PreProceed(httpContext);

            //检测时否包含"Authorization"请求头
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                PostProceed(httpContext);
                return _next(httpContext);
            }

            //var tokenHeader = httpContext.Request.Headers["Authorization"].ToString();
            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                if (tokenHeader.Length >= 128)
                {
                    //Console.WriteLine($"{DateTimeOffset.Now} token: {tokenHeader}.");
                    TokenModelJwt tm = JwtHelper.SerializeJwt(tokenHeader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTimeOffset.Now} middleware wrong: {ex.ToString()}.");
            }

            PostProceed(httpContext);
            return _next(httpContext);
        }
    }
}
