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

            using(var game = ActivatorUtilities.GetServiceOrCreateInstance<Game>(host.Services))
                game.Run();
        }

        public static IConfiguration BuildConfiguration(IConfigurationBuilder builder) {
            return
                builder
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(
                            "appsettings.json", 
                            optional: false, 
                            reloadOnChange: true
                        )
                    .Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<ILoggerFactory>(LoggerFactory.Create(it => it.AddConsole()));
                        services.AddSingleton<Game, PongGame>();
                        services.AddSingleton<GraphicsDeviceManager>();

                        var configuration = BuildConfiguration(new ConfigurationBuilder());
                        services.AddSingleton<IConfiguration>(configuration);
                    }
                );
    }
}
