using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JianShen.Swarm.Common.Config;
using JianShen.Swarm.Common.DataBase;
using JianShen.Swarm.IRepository;
using JianShen.Swarm.IService;
using JianShen.Swarm.Repository;
using JianShen.Swarm.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JianShen.Swarm.WebApi
{
    /// <summary>
    /// �ο���ַ��https://www.cnblogs.com/hbb0b0/p/8391598.html
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                //���л�����               
                .AddNewtonsoftJson(options =>
                {
                    // ����ѭ������
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //��ʹ���շ� .net coreĬ��ʹ���շ� ����ĸСд
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    // ����ʱ���ʽ
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    // ���ֶ�Ϊnullֵ�����ֶβ��᷵�ص�ǰ��
                    // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            //services.AddOptions(); //.net core 3.0����Ҫ��
            services.Configure<WebApiOption>(Configuration.GetSection("WebAPI")); //���ַ���������ע��DI��������IOptions<WebApiOption> optionȡֵ

            //��ȡ����
            //����1  WebApiOption config = Configuration.GetSection("WebAPI").Get<WebApiOption>();
            //����2
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            WebApiOption config = serviceProvider.GetService<IOptions<WebApiOption>>().Value;

            AddDBService(services, config);
            AddCorsService(services, config);

        }

        private void AddDBService(IServiceCollection services, WebApiOption config)
        {
            //���ݿ�������ȫ������
            services.AddSingleton<IDapperContext>(_ => new DapperContext(config));

            services.AddScoped<IDepartmentRep, DepartmentRep>();
            services.AddScoped<IDepartmentService, DepartmentService>();
        }

        private void AddCorsService(IServiceCollection services, WebApiOption config)
        {
            //���cors����
            services.AddCors(options =>
            {
                options.AddPolicy(WebApiOption.CORS_POLICY_NAME,
                            p => p.WithOrigins(config.Cors.Original)
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                 //.WithExposedHeaders("token")
                                 );

            });
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
