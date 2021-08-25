using Demo.Api.HostedServices;
using Demo.Core.Config;
using Demo.Data.DatabaseContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Demo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // SeriLog implementasyonu
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "DemoApi.Project")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(
                        new Uri(EnvVars.GetEnvironmentVariable(EnvVars.ElasticSearchUri)))
                    {
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        AutoRegisterTemplate = true,
                        TemplateName = "serilog-events-template",
                        IndexFormat = "demoapi-log-{0:yyyy.MM.dd}"
                    })
                .MinimumLevel.Verbose()
                .CreateLogger();
            Log.Information("WebApi Starting...");

            //try
            //{
            //    BuildWebHost(args).Run();
            //}
            //catch (Exception e)
            //{
            //    Log.Error(e, "@e");
            //}





            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri())
            //    {
            //        AutoRegisterTemplate = true,
            //        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{DateTime.UtcNow:yyyy-MM}"
            //    })
            //    .Enrich.WithProperty("Environment", environment)
            //    .CreateLogger();
            // end of Serilog implementasyonu

            try
            {
                Log.Information("Starting up");

                var host = CreateHostBuilder(args).Build();

                InitializeDatabase(host);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();
                }).ConfigureServices(services =>
                {
                    services.AddHostedService<SampleHostedService>();
                });

        private static void InitializeDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DemoContext>();
                    context.Database.EnsureCreated();
                    DataGenerator.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
