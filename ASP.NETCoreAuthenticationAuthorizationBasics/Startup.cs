using ASP.NETCoreAuthenticationAuthorizationBasics.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreAuthenticationAuthorizationBasics
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)

        {

            services.AddDbContext<AppDbContext>( config =>
            {
                config.UseInMemoryDatabase("Memory");
            });


            //AddIdentity registers the services
            services.AddIdentity<IdentityUser, IdentityRole>(config=> {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            //this configuration allows you to do configure the cookie add identity
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "MyCookie";
                config.LoginPath = "/Home/Login";
            });


            //Configure the cookie without identity
            //services.AddAuthentication("CookieAuth")
            //    .AddCookie("CookieAuth", config =>
            //    {
            //        config.Cookie.Name = "MyCookie";
            //        config.LoginPath = "/Home/Authenticate";
            //    });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //who are you?
            app.UseAuthentication();

            //are you allowed?
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

}
