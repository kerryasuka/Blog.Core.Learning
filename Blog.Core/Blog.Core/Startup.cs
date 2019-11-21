using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Common;
using Blog.Core.Extensions;
using Blog.Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using static Blog.Core.SwaggerHelper.CustomApiVersion;

namespace Blog.Core
{
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerSetup();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //根据版本名称倒序 遍历展示
                var ApiName = AppSettings.App(new string[] { "Startup", "ApiName" });
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");
                //路径配置，设置为空，表示直接在根域名(如localhost:5000)访问该文件，注意localhost:5000/swagger是访问不到的，去launchSettings.json把lauchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                //这里的RoutePrefix的值要和launchSettings.json中的launchUrl的值对应，否则无法打开
                c.RoutePrefix = "swagger";
            });
            #endregion

            app.UseRouting();

            //自定义认证中间件可以尝试，但不推荐
            //app.UseJwtTokenAuth();
            //先开启认证中间件
            app.UseAuthentication();
            //再开启授权中间件
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
