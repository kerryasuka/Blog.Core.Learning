using Blog.Core.AuthHelper.Policys;
using Blog.Core.Common;
using Blog.Core.Common.DB;
using Blog.Core.Common.GlobalVar;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Extensions
{
    /*
     *设置认证和授权服务，必须三步走，授权 + 配置认证服务 + 开启授权中间件
     *对于自定义的中间件的说明：其不能验证过期时间，故推荐使用官方认证和授权中间件
     */
    /// <summary>
    /// 启动授权服务 
    /// </summary>
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //读取配置文件
            var symmetricKeyAsBase64 = AppSecretConfig.Audience_Secret_String;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            //AddAuthenticationByDb(services, signingKey);
            AddAuthorizationByCode(services, signingKey);
        }

        /*
        *如果不想使用数据库，仅仅想在代码里配置授权，可以按照以下步骤进行：
        * 1、授权
        * 1.1、基于角色的API授权
        * 在指定的接口上，配置特性即可，比如：[Authorize(Role = "Admin, System, Others")]
        *
        * 1.2、基于策略的授权(简单版)
        * 如果感觉"Admin, System, Others"，这样的字符串太长的话，可以将这些融合到简单策略里
        * 具体配置，详询1.2、基于策略的授权(简单版)，然后在接口上，配置特性：[Authorize(Policy = "A_S_O")]
        * 
        * 2、认证
        * 配置Bearer认证服务，具体代码详询2.1认证
        * 
        * 3、中间件
        * 开启中间件
        */
        public static void AddAuthorizationByCode(IServiceCollection services, SymmetricSecurityKey signingKey)
        {
            //角色与接口的权限要求参数
            var permissionRequirement = GetParameters(signingKey);

            //1、授权
            //1.1、基于角色的API授权
            //这个很简单，其他什么都不用做，只需要在API层的controller上增加特性即可，注意，只能是角色的：
            //[Authorize(Role = "Admin, System")]

            //1.2、基于策略的授权(简单版)
            //和1.1、基于角色的API授权异曲同工，好处就是不用在controller中写多个roles
            //添加以下代码
            //然后在controller上面加上特性
            //[Authorize(Policy = "Admin")]
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
                options.AddPolicy("A_S_O", policy => policy.RequireRole("Admin", "System", "Others"));
            });

            Authorize(services, signingKey, permissionRequirement);
        }

        /*
        *如果要把权限配置到数据库，步骤如下：
        * 1、授权
        * 1.3、复杂的策略授权
        * 具体详询1.3、复杂的策略授权
        * 
        * 2、认证
        * 配置Bearer认证服务，具体代码详询2.1认证
        * 
        * 3、中间件
        * 开启中间件
        */
        public static void AddAuthenticationByDb(IServiceCollection services, SymmetricSecurityKey signingKey)
        {
            //角色与接口的权限要求参数
            var permissionRequirement = GetParameters(signingKey);

            //1.3、复杂的策略授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.Name, policy => policy.Requirements.Add(permissionRequirement));
            });

            Authorize(services, signingKey, permissionRequirement);
        }

        /// <summary>
        /// 认证和注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="signingKey"></param>
        /// <param name="permissionRequirement"></param>
        private static void Authorize(IServiceCollection services, SymmetricSecurityKey signingKey, PermissionRequirement permissionRequirement)
        {
            //令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = permissionRequirement.Issuer,//发行者
                ValidateAudience = true,
                ValidAudience = permissionRequirement.Audience,//订阅者
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
            };

            //2、认证
            //2.1、core自带官方JWT认证
            //开启Bearer认证
            services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = nameof(ApiResponseHandler);
                    o.DefaultForbidScheme = nameof(ApiResponseHandler);
                })
                //添加JwtBearer服务
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = tokenValidationParameters;
                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            //如果过期，则把<是否过期>添加到返回的头部信息中
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });

            //2.2、IdentityServer4认证(暂时忽略)
            //services
            //    .AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:5002";
            //        options.RequireHttpsMetadata = false;
            //        options.ApiName = "Blog.Core.Api";
            //    });

            //注入权限处理器
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);
        }

        /// <summary>
        /// 读取配置文件获取参数
        /// </summary>
        /// <param name="signingKey"></param>
        /// <returns></returns>
        private static PermissionRequirement GetParameters(SymmetricSecurityKey signingKey)
        {
            var issuer = AppSettings.App(new string[] { "Audience", "Issuer" });
            var audience = AppSettings.App(new string[] { "Audience", "Audience" });

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            //如果要数据库动态绑定，这里先留个空，后边处理器里动态绑定
            var permissions = new List<PermissionItem>();

            return new PermissionRequirement(
               "/api/denied",//拒绝授权的跳转地址(目前无用)
               permissions,
               ClaimTypes.Role,//基于角色的授权
               issuer,//发行人
               audience,//订阅者
               signingCredentials,//签名凭据
               expiration: TimeSpan.FromSeconds(60 * 60)
               );
        }
    }
}
