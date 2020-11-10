using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using StrategyGame.DAL;

namespace StrategyGame.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateHostBuilder(args)
                .Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var hostingEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

                if (hostingEnvironment.IsDevelopment())
                {
                    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                }
            }

            webHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext()
                        .WriteTo.ColoredConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
