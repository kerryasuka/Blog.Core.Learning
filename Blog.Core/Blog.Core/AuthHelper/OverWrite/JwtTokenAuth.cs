using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Core.AuthHelper.OverWrite
{
    /// <summary>
    /// 中间件
    /// 原作为自定义认证的中间件
    /// 现作为检查header token使用
    /// </summary>
    public class JwtTokenAuth
    {
        private readonly RequestDelegate _next;

        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 中间件前处理
        /// </summary>
        /// <param name="next"></param>
        private void PreProceed(HttpContext next)
        {
            //Console.WriteLine($"{DateTimeOffset.Now} middleware invoke prepoceed.");
            //...
        }

        /// <summary>
        /// 中间件后处理
        /// </summary>
        /// <param name="next"></param>
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

                    //授权
                    var claimList = new List<Claim>();
                    var claim = new Claim(ClaimTypes.Role, tm.Role);
                    claimList.Add(claim);
                    var identity = new ClaimsIdentity(claimList);
                    var principal = new ClaimsPrincipal(identity);
                    httpContext.User = principal;
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
