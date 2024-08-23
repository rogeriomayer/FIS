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
using Microsoft.OpenApi.Models;
using SwaggerFilter.Filters;
using System;
using System.IO;
using System.Text;

namespace FMC.FIS.API
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

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "API FIS",
                        Version = "v1",
                        Description = "API para aplicações ligadas a Inovação em Cobrança"
                    });
              options.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, "FMC.FIS.API.xml"));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });

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

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Billet}/");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Person}/");

            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                if (env.IsDevelopment())
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FIS");
                else
                    c.SwaggerEndpoint("/FIS/swagger/v1/swagger.json", "FIS");

            });

            GetSettings();
        }

        public void GetSettings()
        {
            Constants.Jwt = Configuration.GetValue<string>("Jwt");
            Constants.TokenExpires = Configuration.GetValue<int>("TokenExpires");

            Constants.UserRecuperaAfinz = Configuration.GetValue<string>("UserRecuperaAfinz");
            Constants.PassRecuperaAfinz = Configuration.GetValue<string>("PassRecuperaAfinz");
            Constants.TokenRecuperaAfinz = Configuration.GetValue<string>("TokenRecuperaAfinz");
            Constants.URL_API_P1 = Configuration.GetValue<string>("URL_API_P1");
            Constants.TOKEN_API_P1 = Configuration.GetValue<string>("TOKEN_API_P1");

            Constants.URL_API_CODE_CONNECT = Configuration.GetValue<string>("URL_API_CODE_CONNECT");
            Constants.TOKEN_CODE_CONNECT = Configuration.GetValue<string>("TOKEN_CODE_CONNECT");
            Constants.USER_CODE_CONNECT = Configuration.GetValue<string>("USER_CODE_CONNECT");
            Constants.PASS_CODE_CONNECT = Configuration.GetValue<string>("PASS_CODE_CONNECT");


            Constants.HOST_SMTP = Configuration.GetValue<string>("HOST_SMTP");
            Constants.PORT_SMTP = Configuration.GetValue<string>("PORT_SMTP");
            Constants.USER_SMTP = Configuration.GetValue<string>("USER_SMTP");
            Constants.PASS_SMTP = Configuration.GetValue<string>("PASS_SMTP");

            Constants.USER_TWW = Configuration.GetValue<string>("USER_TWW");
            Constants.PASS_TWW = Configuration.GetValue<string>("PASS_TWW");

            Constants.UserCobmaisCredz = Configuration.GetValue<string>("UserCobmaisCredz");
            Constants.PassCobmaisCredz = Configuration.GetValue<string>("PassCobmaisCredz");
            Constants.UrlApiCobmaisCredz = Configuration.GetValue<string>("UrlApiCobmaisCredz");

        }

    }
}
