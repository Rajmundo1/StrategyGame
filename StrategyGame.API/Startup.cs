using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using StrategyGame.API.Common;
using StrategyGame.API.Infrastructure.Middlewares;
using StrategyGame.API.Infrastructure.Services;
using StrategyGame.BLL;
using StrategyGame.BLL.BackgroundJobs.Interfaces;
using StrategyGame.BLL.Hubs;
using StrategyGame.BLL.ValidationDtos;
using StrategyGame.DAL;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StrategyGame.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac
            builder.RegisterModule(new BllModule());
            builder.RegisterModule(new DalModule());
            //TODO
            //builder.RegisterType<TwoFactorAuth>()
            //    .AsSelf()
            //    .SingleInstance();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            // Add Hangfire services
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("Default"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();


            services.AddCors(o => o.AddPolicy("AllowMySpecificOrigins", builder =>
            {
                builder.WithOrigins(
                    Configuration.GetSection("Frontend")["Url"],
                    Configuration.GetSection("Frontend")["SecureUrl"]
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            services.AddAutoMapper(typeof(MapperProfiles));

            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddScoped<IUserStore<User>, ApplicationUserStore>();
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Normal";
                    options.DefaultChallengeScheme = "Normal";
                })
                .AddJwtBearer("Normal", cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetSection("Authentication")["JwtNormalIssuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration.GetSection("Authentication")["JwtNormalIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Authentication")["JwtKey"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                })
                .AddJwtBearer("Login", cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetSection("Authentication")["JwtLoginIssuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration.GetSection("Authentication")["JwtLoginIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Authentication")["JwtKey"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            services
                .AddAuthorization(options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes("Normal")
                        .Build();

                    options.AddPolicy("Login", new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes("Login")
                        .Build());
                });

            // Register the Swagger generator, defining 1 Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "STRATEGYGAME API", Version = "v1" });

                c.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Basic",
                    Name = "Authorization",
                    Description = "Authorization header. \"Bearer {token}\"",
                    In = ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basicAuth" }
                            }, new List<string>() }
                    });
            });

            services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining(typeof(LoginDtoValidator));
                })
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });

            services.Configure<SpaConfiguration>(Configuration.GetSection("SpaConfiguration"));
            services.Configure<BackGroundJobConfiguration>(Configuration.GetSection("BackgroundJobs"));
            services.AddTransient<IIdentityService, IdentityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                            IServiceProvider serviceProvider,
                            IRecurringJobManager recurringJobManager,
                            IOptions<BackGroundJobConfiguration> backgroundJobConfiguration)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "STRATEGYGAME API V1");
            });

            app.UseMiddleware<SpaConfigMiddleware>();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSerilogRequestLogging(options =>
            {
                // Customize the message template
                options.MessageTemplate = "Handled {RequestPath}";

                // Emit debug-level events instead of the defaults
                options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

                // Attach additional properties to the request completion event
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                };
            });

            app.UseRouting();

            app.UseCors("AllowMySpecificOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            // Pass background tasks to Hangfire
            recurringJobManager.AddOrUpdate(
                "New Round",
                () => serviceProvider.GetService<INewRoundJob>().NewRound(),
                backgroundJobConfiguration.Value.NewRoundJobInterval);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
                endpoints.MapHub<MyHub>("/api/newround");
            });

            app.UseSpa(spa => { });
        }
    }
}
