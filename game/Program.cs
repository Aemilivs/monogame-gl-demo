using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace game
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            BuildConfiguration(new ConfigurationBuilder());

            using(var game = ActivatorUtilities.GetServiceOrCreateInstance<Game>(host.Services))
                game.Run();
        }

        public static void BuildConfiguration(IConfigurationBuilder builder) {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                        "appsettings.json", 
                        optional: false, 
                        reloadOnChange: true
                    );
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<ILoggerFactory>(LoggerFactory.Create(it => it.AddConsole()));
                        services.AddTransient<Game, ExampleGame>();
                    }
                );
    }
}
