using Blog.Core.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Linq;
using static Blog.Core.SwaggerHelper.CustomApiVersion;

namespace Blog.Core.Extensions
{
    /// <summary>
    /// Swagger启动服务
    /// </summary>
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = AppContext.BaseDirectory;
            var basePath2 = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            var ApiName = AppSettings.App(new string[] { "Startup", "ApiName" });

            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        //{ApiName}定义的全局变量
                        Version = version,
                        Title = $"{ApiName} 接口文档 —— .Net Core 3.0",
                        Description = $"{ApiName} Http Api " + version,
                        Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://log.csdn.net/kerryasuka") },
                        License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://blog.csdn.net/kerryasuka") }
                    });
                    c.OrderActionsBy(o => o.RelativePath);
                });

                //配置文档路径
                //文档名称要和生成路径的文档名称对应
                var xmlPath = Path.Combine(basePath, "Blog.Core.xml");
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                var xmlModelPath = Path.Combine(basePath, "Blog.Core.Model.xml");
                c.IncludeXmlComments(xmlModelPath, true);

                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                //Token绑定到ConfigureServices
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输)，直接在下框中输入：Bearer {token} (注意两者之间是一个空格)",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header,  //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                });
            });
        }
    }
}
