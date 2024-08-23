using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FMC.Fis
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>{
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.IsEssential = true;

            });
            
            //services.AddHttpsRedirection(options =>
            //{
            //    options.HttpsPort = 443;
            //});                           // <=== Add this (AddHttpsRedirection) =====


            services.AddRouting(routeOptions => routeOptions.LowercaseUrls = true);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDistributedMemoryCache();
            //services.AddMemoryCache();
            
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            //services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseHttpContextItemsMiddleware();

            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "negociar-agora",
                   template: "negociar-agora",
                   defaults: new { controller = "NegociarAgora", action = "Index" }
                );

                routes.MapRoute(
                   name: "pepsico",
                   template: "pepsico",
                   defaults: new { controller = "NegociarAgora", action = "Index" }
                );

                routes.MapRoute(
                   name: "multiloja",
                   template: "multiloja",
                   defaults: new { controller = "NegociarAgora", action = "Index" }
                );

                routes.MapRoute(
                   name: "simplic",
                   template: "simplic",
                   defaults: new { controller = "NegociarAgora", action = "Index" }
                );

                routes.MapRoute(
                   name: "bradesco",
                   template: "bradesco",
                   defaults: new { controller = "NegociarAgora", action = "Index" }
                );
                routes.MapRoute(
                   name: "bradesco-super-dia",
                   template: "bradesco-super-dia",
                   defaults: new { controller = "NegociarAgora", action = "Index" }
                );
                routes.MapRoute(
                   name: "cartoes-bradescard",
                   template: "cartoes-bradescard",
                   defaults: new { controller = "NegociarAgora", action = "Index" }
                );
                routes.MapRoute(
                   name: "bradescard",
                   template: "bradescard/{id}",
                   defaults: new { controller = "NegociarAgora", action = "Index", id = UrlParameter.Optional }
                );
                
                routes.MapRoute(
                   name: "moneyman",
                   template: "moneyman",
                   defaults: new { controller = "NegociarAgora", action = "Index" }
                );
                routes.MapRoute(
                   name: "credjet",
                   template: "credjet",
                   defaults: new { controller = "NegociarAgora", action = "Index" }
                );
                routes.MapRoute(
                    name: "negocie",
                    template: "negocie/{d}",
                    defaults: new { controller = "NegociarAgora", action = "Index", d = UrlParameter.Optional }
                 );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
