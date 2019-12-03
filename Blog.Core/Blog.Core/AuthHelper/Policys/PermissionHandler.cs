using Blog.Core.Common.Helper;
using Blog.Core.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Core.AuthHelper.Policys
{
    /// <summary>
    /// 权限授权处理器
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider AuthenticationSchemeProvider { get; set; }
        private readonly IHttpContextAccessor m_Accessor;

        /// <summary>
        /// services层注入
        /// </summary>
        public IRoleModulePermissionServices RoleModulePermissionServices { get; set; }

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="authenticationSchemeProvider"></param>
        /// <param name="roleModulePermissionServices"></param>
        /// <param name="accessor"></param>
        public PermissionHandler(IAuthenticationSchemeProvider authenticationSchemeProvider, IRoleModulePermissionServices roleModulePermissionServices, IHttpContextAccessor accessor)
        {
            m_Accessor = accessor;
            this.AuthenticationSchemeProvider = authenticationSchemeProvider;
            this.RoleModulePermissionServices = roleModulePermissionServices;
        }

        /// <summary>
        /// 异步需求处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            /*
             * 首先必须在controller上配置Authorize，可以是策略授权，也可以是角色基本授权
             * 1、开启公约，startup中的全局授权过滤公约：o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
             * 2、不开启公约，使用IHttpContextAccessor，也能实现效果
             */

            //更新最新的角色和接口列表
            var data = await RoleModulePermissionServices.GetRoleModule();
            var list = data.Where(w => w.IsDeleted == false).OrderBy(o => o.Id).Select(s => new PermissionItem()
            {
                Role = s.Role?.Name,
                Url = s.Module?.LinkUrl,
            }).ToList();

            requirement.Permissions = list;

            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            //var filterContext = context.Resource as AuthorizationFilterContext;
            var httpContext = (context.Resource as AuthorizationFilterContext)?.HttpContext;

            if (httpContext == null)
            {
                httpContext = m_Accessor.HttpContext;
            }

            //请求Url
            if (httpContext != null)
            {
                var requestUrl = httpContext.Request.Path.Value.ToLower();
                //判断请求是否停止
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await AuthenticationSchemeProvider.GetRequestHandlerSchemesAsync())
                {
                    if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                    {
                        context.Fail();
                        return;
                    }
                }

                //判断请求是否拥有凭据，即有没有登陆
                var defaultAuthenticate = await AuthenticationSchemeProvider.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    //result.Princple不为空即登陆成功
                    if (result?.Principal != null)
                    {
                        httpContext.User = result.Principal;

                        //权限中是否存在请求的Url
                        //if (requirement.Permissions.GroupBy(g => g.Url).Where(w => w.Key?.ToLower() == requestUrl).Count() > 0)
                        if (true)
                        {
                            //获取当前用户的角色信息
                            var currentUserRoles = httpContext.User.Claims.Where(w => w.Type == requirement.ClaimType).Select(s => s.Value).ToList();

                            var isMatchRole = false;
                            var permissionRoles = requirement.Permissions.Where(w => currentUserRoles.Contains(w.Role));
                            foreach (var role in permissionRoles)
                            {
                                try
                                {
                                    if (Regex.Match(requestUrl, role.Url?.ObjToString().ToLower())?.Value == requestUrl)
                                    {
                                        isMatchRole = true;
                                        break;
                                    }
                                }
                                catch (RegexMatchTimeoutException ex)
                                {
                                    Console.WriteLine($"RegexMatchError: {ex.ToString()}");
                                }
                            }

                            //验证权限
                            //if(currentUserRoles.Count <= 0 || requirement.Permissions.Where(w => currentUserRoles.Contains(w.Role) && w.Url.ToLower() == requestUrl).Count() <= 0)
                            if (currentUserRoles.Count <= 0 || !isMatchRole)
                            {
                                context.Fail();
                                return;
                            }
                        }

                        //判断过期时间(这里仅仅是最坏验证原则，你可以不要这个if else的判断，因为我们使用的官方验证，Token过期后上边的result?.Principal就为null了，讲不到这里，因此这里其实可以不用验证过期时间，只是做最后研究判断)
                        if ((httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) != null && DateTime.Parse(httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) >= DateTime.Now)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                            return;
                        }
                        return;
                    }
                }

                //判断没有登陆时，是否访问登陆的url，并且是Post请求，form表单提交类型，否则为失败
                if(!requestUrl.Equals(requirement.LoginPath.ToLower(), StringComparison.Ordinal) && (!httpContext.Request.Method.Equals("POST") || !httpContext.Request.HasFormContentType))
                {
                    context.Fail();
                    return;
                }
            }

            context.Succeed(requirement);
        }
    }
}
