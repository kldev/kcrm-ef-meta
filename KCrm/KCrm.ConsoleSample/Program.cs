using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using KCrm.Aegis.Migrator;
using KCrm.Data.Projects;
using KCrm.Data.Tags;
using KCrm.Data.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KCrm.ConsoleSample {
    class Program {
        private static IConfigurationRoot configuration;
        private static IServiceProvider serviceProvider;

        static async Task Main(string[] args) {
            var serviceCollection = new ServiceCollection ( );
            ConfigureServices (serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider ( );

            var stopwatch = new Stopwatch ( );
            stopwatch.Start ( );

            await RunSamples ( );

            Console.WriteLine ("Program executed in: {0:hh\\:mm\\:ss} {1} ms", stopwatch.Elapsed,
                stopwatch.ElapsedMilliseconds);
        }

        private static async Task RunSamples() {
            var factory = serviceProvider.GetService<ILoggerFactory> ( );
            var logger = factory.CreateLogger<TagSample> ( );

            await new TagSample ( ).EnsureDeleted<TagContext> (configuration, logger);

            await new TagSample ( ).RunAsync<TagContext> (configuration, logger);
            await new ProjectSample ( ).RunAsync<ProjectContext> (configuration, logger);
            await new UserSample ( ).RunAsync<AppUserContext> (configuration, logger);

            new Migrator ( ).UpdateDatabase (configuration);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection) {
            // Add logging
            serviceCollection.AddSingleton (LoggerFactory.Create (builder => {
                builder
                    .AddConsole ( );
            }));

            serviceCollection.AddLogging ( );

            // Build configuration
            configuration = new ConfigurationBuilder ( )
                .SetBasePath (Directory.GetParent (AppContext.BaseDirectory).FullName)
                .AddJsonFile ("appsettings.json", false)
                .Build ( );

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton (configuration);

        }
    }
}
