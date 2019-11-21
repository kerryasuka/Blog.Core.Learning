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
        /// ���캯��
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
                //���ݰ汾���Ƶ��� ����չʾ
                var ApiName = AppSettings.App(new string[] { "Startup", "ApiName" });
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");
                //·�����ã�����Ϊ�գ���ʾֱ���ڸ�����(��localhost:5000)���ʸ��ļ���ע��localhost:5000/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��lauchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
                //�����RoutePrefix��ֵҪ��launchSettings.json�е�launchUrl��ֵ��Ӧ�������޷���
                c.RoutePrefix = "swagger";
            });
            #endregion

            app.UseRouting();

            //�Զ�����֤�м�����Գ��ԣ������Ƽ�
            //app.UseJwtTokenAuth();
            //�ȿ�����֤�м��
            app.UseAuthentication();
            //�ٿ�����Ȩ�м��
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
