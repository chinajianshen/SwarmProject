using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrivilegeManagement.Infrastructure.CustomMiddleware;
using PrivilegeManagement.Models;

namespace PrivilegeManagement
{
    /// <summary>
    /// 引用路径：https://www.cnblogs.com/axzxs2001/p/7482771.html
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

            //添加认证Cookie信息
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = new PathString("/login");
                        options.AccessDeniedPath = new PathString("/denied");
                    });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //验证中间件
            app.UseAuthentication();

            //添加权限中间件, 一定要放在app.UseAuthentication后
            //一定要在app.UseAuthentication下面添加验证权限的中间件，因为UseAuthentication要从Cookie中加载通过验证的用户信息到Context.User中，所以一定放在加载完后才能去验用户信息（当然自己读取Cookie也可以）
            app.UseAccessPermission(new PermissionMiddlewareOption()
            {
                LoginAction = @"/login",
                NoPermissionAction = @"/denied",
                //这个集合从数据库中查出所有用户的全部权限
                UserPerssions = new List<UserPermission>()
                 {
                         new UserPermission { Url = "/", UserName = "zh" },
                         new UserPermission { Url = "/home/contact", UserName = "zh" },
                         new UserPermission { Url = "/home/about", UserName = "zh2" },
                         new UserPermission { Url = "/", UserName = "zh2" }

                  }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
