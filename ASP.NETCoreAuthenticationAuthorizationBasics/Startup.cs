
using ASP.NETCoreAuthenticationAuthorizationBasics.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.NETCoreAuthenticationAuthorizationBasics
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)

        {


            //Configure the cookie without identity
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "MyCookie";
                    config.LoginPath = "/Home/Authenticate";
                });


            services.AddAuthorization(config =>
            {
                //    var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //    var defaultAuthPolicy = defaultAuthBuilder
                //            .RequireAuthenticatedUser()
                //            .RequireClaim(ClaimTypes.DateOfBirth)
                //            .Build();

                //    config.DefaultPolicy = defaultAuthPolicy;

                config.AddPolicy("Claim.DateOfBirth.Policy", policyBuilder =>
                {
                    //policyBuilder.AddRequirements(new CustomRequireClaim(ClaimTypes.DateOfBirth));
                    policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
                });
            });

            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

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
