using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using System.Collections.Generic;
using System.Linq;

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
            /* services.Configure<TrackerOptions>(options => Configuration.GetSection("Tracker").Bind(options));*/
            services.AddControllers();
            /*
             services.AddDbContext<TrackerDataContext>(options =>
                 options.UseMySQL(
                         Configuration.GetConnectionString("WebApi")));

             services.AddDbContext<TrackerIdentityContext>(options =>
                 options.UseMySQL(
                         Configuration.GetConnectionString("WebApiIdentity")));
            */

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
            });

            //Maybe load profile
            //Additional config snipped




            services.AddAuthorization();
            /*
            services.AddScoped<IDownloadService, DownloadService>();
            services.AddScoped<IFullTextSearchService, FullTextSearchService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ITorrentStatsService, TorrentStatsService>();
            services.AddScoped<ITorrentInfoService, TorrentInfoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITorrentFileService, TorrentFileService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IApprovingService, ApprovingService>();
            services.AddScoped<IDiscordService, DiscordService>();*/
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
                app.UseExceptionHandler("/Error");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            bool useHttps = true;
            if (env.IsDevelopment())
            {
                useHttps = false;
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlackZone API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpMetrics();
            app.UseMetricServer();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers()
            );
        }
    }
}
