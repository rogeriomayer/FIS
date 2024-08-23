using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FMC.CREDZ.API
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.IsEssential = true;
            });


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();
            services.AddRouting(routeOptions => routeOptions.LowercaseUrls = true);
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDistributedMemoryCache();
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMemoryCache();
            //services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(a =>
            {
                a.RequireHttpsMetadata = false;
                a.SaveToken = true;
                a.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Jwt"))),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(x =>
            {
                x.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.Configure<Constants>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseCors();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/");

               
            });

            GetSettings();
        }

        public void GetSettings()
        {
            Constants.Jwt = Configuration.GetValue<string>("Jwt");
            Constants.TokenExpires = Configuration.GetValue<int>("TokenExpires");

            Constants.UserRecuperaCredZ = Configuration.GetValue<string>("UserRecuperaCredZ");
            Constants.PassRecuperaCredZ = Configuration.GetValue<string>("PassRecuperaCredZ");
            Constants.TokenRecuperaCredZ = Configuration.GetValue<string>("TokenRecuperaCredZ");
            Constants.URL_API_P1 = Configuration.GetValue<string>("URL_API_P1");
            Constants.TOKEN_API_P1 = Configuration.GetValue<string>("TOKEN_API_P1");

            Constants.URL_API_CODE_CONNECT = Configuration.GetValue<string>("URL_API_CODE_CONNECT");
            Constants.TOKEN_CODE_CONNECT = Configuration.GetValue<string>("TOKEN_CODE_CONNECT");
            Constants.USER_CODE_CONNECT = Configuration.GetValue<string>("USER_CODE_CONNECT");
            Constants.PASS_CODE_CONNECT = Configuration.GetValue<string>("PASS_CODE_CONNECT");


        }

    }
}
