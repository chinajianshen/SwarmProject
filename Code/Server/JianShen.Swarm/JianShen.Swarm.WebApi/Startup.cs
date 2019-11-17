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
    /// 参考网址：https://www.cnblogs.com/hbb0b0/p/8391598.html
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
                //序列化设置               
                .AddNewtonsoftJson(options =>
                {
                    // 忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰 .net core默认使用驼峰 首字母小写
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    // 设置时间格式
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    // 如字段为null值，该字段不会返回到前端
                    // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            //services.AddOptions(); //.net core 3.0不需要添
            services.Configure<WebApiOption>(Configuration.GetSection("WebAPI")); //此种方法将属性注入DI，可以用IOptions<WebApiOption> option取值

            //获取配置
            //方法1  WebApiOption config = Configuration.GetSection("WebAPI").Get<WebApiOption>();
            //方法2
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            WebApiOption config = serviceProvider.GetService<IOptions<WebApiOption>>().Value;

            AddDBService(services, config);
            AddCorsService(services, config);

        }

        private void AddDBService(IServiceCollection services, WebApiOption config)
        {
            //数据库上下文全局设置
            services.AddSingleton<IDapperContext>(_ => new DapperContext(config));

            services.AddScoped<IDepartmentRep, DepartmentRep>();
            services.AddScoped<IDepartmentService, DepartmentService>();
        }

        private void AddCorsService(IServiceCollection services, WebApiOption config)
        {
            //添加cors服务
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
