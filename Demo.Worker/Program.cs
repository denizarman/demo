using Demo.Core.Config;
using Demo.Data.DatabaseContext;
using Demo.Worker.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<DummyWorker>();
                    services.AddDbContext<DemoContext>(options =>
                    options.UseNpgsql(EnvVars.GetEnvironmentVariable(EnvVars.DemoContextConnectionString)), ServiceLifetime.Transient);
                });
    }
}
