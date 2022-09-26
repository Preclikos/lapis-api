using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using WebApi.Database.Interfaces;
using WebApi.Database.Repositories;
using WebApi.Databases;
using WebApi.Middleware;
using WebApi.Services;
using WebApi.Services.Interfaces;

namespace LapisApi
{
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
            services.AddControllers();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("192.168.100.250"));
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            var origins = Configuration.GetSection("AllowOrigin");
            var originArray = origins.AsEnumerable().Where(w => !string.IsNullOrEmpty(w.Value)).Select(s => s.Value).ToArray();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(originArray);
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            { //<-- NOTE 'Add' instead of 'Configure'
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Lapis WebAPI",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.MetadataAddress = "https://login.lapis.report/realms/lapis/.well-known/openid-configuration";
                o.Authority = "https://login.lapis.report/realms/lapis";
                o.Audience = "account";
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
                /*o.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        ctx.Request.Headers.Add("OnTokenValidated", new Microsoft.Extensions.Primitives.StringValues("True"));
                    },
                   OnChallenge = async ctx =>
                   {
                       ctx.Request.Headers.Add("OnTokenValidated", new Microsoft.Extensions.Primitives.StringValues("True"));
                   },
                   OnMessageReceived = async ctx =>
                   {
                       ctx.Request.Headers.Add("OnTokenValidated", new Microsoft.Extensions.Primitives.StringValues("True"));
                   }

                };*/
            });

            services.AddHttpContextAccessor();
            services.AddAuthorization();

            services.AddTransient<UserNameMiddleware>();
            services.AddTransient<ProxyMiddleware>();

            services.AddMemoryCache();
            services.AddSingleton<LapisDataContext>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserCacheRepository, UserCacheRepository>();

            services.AddTransient<ILapisRepository, LapisRepository>();
            services.AddTransient<ILapisCodeRepository, LapisCodeRepository>();
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<ILapisImageRepository, LapisImageRepository>();
            services.AddTransient<ILapisLocationRepository, LapisLocationRepository>();
            services.AddTransient<IUserImageRepository, UserImageRepository>();

            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<ILapisService, LapisService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            bool useHttps = true;
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                useHttps = false;
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    var scheme = useHttps ? "https" : httpReq.Scheme;
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{scheme}://{httpReq.Host.Value}" } };
                });
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lapis API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpMetrics();
            app.UseMetricServer();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseUserNameMiddleware();
            app.UseProxyMiddleware();

            app.UseForwardedHeaders();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers()
            );
        }
    }
}
