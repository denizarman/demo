using AutoMapper;
using Demo.Api.Dtos;
using Demo.Api.Filters;
using Demo.Api.HostedServices;
using Demo.Api.Middlewares;
using Demo.Core.Config;
using Demo.Data.DatabaseContext;
using Demo.Data.Repositories.Implementations;
using Demo.Data.Repositories.Interfaces;
using Demo.Service.Services.Implementations;
using Demo.Service.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Minio;
using ServiceStack.Redis;
using System.Reflection;

namespace Demo.Api
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
            #region Swagger Configuration

            services.AddControllers();
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Demo API",
                    Description = "ASP.NET Core 3.1 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                swagger.OperationFilter<SwaggerFileOperationFilter>();
            });

            #endregion

            #region AutoMapper Configuration

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                // Add all Profiles from the Assembly containing this Type
                cfg.AddMaps(Assembly.GetAssembly(typeof(DepartmentDto)));
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region Serialization Confituration

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            #endregion

            #region IoC Configuration

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentService, DocumentService>();

            #endregion

            services.AddHostedService<SampleHostedService>();

            #region AppSettings Configuration

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            #endregion

            #region DbContext Configuration

            var databaseProvider = EnvVars.GetEnvironmentVariable(EnvVars.DatabaseProvider);
            if (databaseProvider == DatabaseProviders.InMemory)
            {
                services.AddDbContext<DemoContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "DemoContext"), ServiceLifetime.Transient);
            }
            else if (databaseProvider == DatabaseProviders.PgSql)
            {
                services.AddDbContext<DemoContext>(options =>
                    options.UseNpgsql(EnvVars.GetEnvironmentVariable(EnvVars.DemoContextConnectionString)), ServiceLifetime.Transient);
            }

            #endregion

            #region Redis Client Configuration

            //services.ConfigureDynamicProxy(config => config.Interceptors.AddTyped<CacheAttribute>());
            //var serviceProvider = services.BuildDynamicProxyProvider();
            services.AddSingleton<IRedisClientsManager>(c =>
                new RedisManagerPool(EnvVars.GetEnvironmentVariable(EnvVars.RedisAddress)));

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
