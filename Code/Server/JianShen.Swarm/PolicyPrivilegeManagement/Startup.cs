using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolicyPrivilegeManagement.Infrastructure.CustomAuthorizeHandler;
using PolicyPrivilegeManagement.Models;

namespace PolicyPrivilegeManagement
{
    /// <summary>
    /// 参考地址：https://www.cnblogs.com/axzxs2001/p/7482777.html
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthorization(options =>
            {
                //Controller怎么用授权策略？  [Authorize(Policy = "RequireClaim")]

                //基于角色策略
                //options.AddPolicy("RequireClaim", policy =>
                //{
                //    policy.RequireRole("admin", "system");
                //});

                //基于用户名 当在认证时 ClaimTypes.Name，设置的不是 张三 则直接拒绝访问
                //options.AddPolicy("RequireClaim", policy =>
                //{
                //    policy.RequireUserName("张三");
                //});

                //基于ClaimType
                //options.AddPolicy("RequireClaim", policy => policy.RequireClaim(ClaimTypes.Country,"中国"));

                //自定义值
                // options.AddPolicy("RequireClaim", policy => policy.RequireClaim("date","2017-09-02")); 


                //自定义授权Handler(相当于PrivilegeManagement项目中AccessPermissionMiddleware中间件)

                //自定义Requirement，userPermission可从数据库中获得
                //var userPermission = new List<UserPermission> {
                //       new UserPermission {  Url="/", UserName="zh"},
                //       new UserPermission {  Url="/home/t1", UserName="zh"},
                //        new UserPermission {  Url="/", UserName="zh2"},
                //        new UserPermission {  Url="/home/t2", UserName="zh2"}
                //    };
                var userPermissions = Configuration.GetSection("UserPermissions").Get<List<UserPermission>>();

                options.AddPolicy("RequireClaim", policy => policy.Requirements.Add(new PermissionRequirement("/Denied", userPermissions)));


            }).AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.LoginPath = new PathString("/Login");
                  options.AccessDeniedPath = new PathString("/Denied");
              });

            //注入授权Handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            //验证中间件
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
