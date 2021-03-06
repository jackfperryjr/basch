using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Basch.Api.Infrastructure;
using Basch.Api.Core.Extensions;
using Basch.Api.Core.Logging;
using Basch.Api.Core.Security;
using Basch.Api.Core.Swagger;

namespace Basch.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment  environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public static IConfiguration Configuration { get; private set; }
        public static IWebHostEnvironment Environment { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddSingleton(Environment);
            services.AddMemoryCache();
            services.Configure<SwaggerSettings>(Configuration.GetSection(nameof(SwaggerSettings)));
            services.Configure<ApplicationMetadata>(Configuration.GetSection(nameof(ApplicationMetadata)));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHttpContextAccessor();

            var jwtSection = Configuration.GetSection(nameof(AppSettings));
            var assembly = Assembly.Load("Basch.Api.Infrastructure");
            services.Configure<AppSettings>(jwtSection);
            services.AddFactories(assembly)
                    .AddStores(assembly);

            services.AddJwtAuthentication(jwtSection)
                    .AddControllers()
                    .AddNewtonsoftJson(opt => {
                        opt.SerializerSettings.ContractResolver = 
                            new DefaultContractResolver
                            {
                                NamingStrategy = new CamelCaseNamingStrategy()
                            };
                        opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });

            services.AddApiVersionWithExplorer()
                    .AddSwaggerOptions()
                    .AddSwaggerGen(x =>
                    {
                        var security = new OpenApiSecurityRequirement()
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header
                                },
                                new List<string>()
                            }
                        };

                        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "JWT Authorization header",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey
                        });

                        x.AddSecurityRequirement(security);
                    });  // Generates Authorize button and makes swagger aware of authorization with JWT

            services.AddControllersWithViews();
            services.AddRouting(x => x.LowercaseUrls = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwaggerDocuments();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());  

            app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto
                });  

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(x => {
                x.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                x.MapControllers();
            });
        }
    }
}