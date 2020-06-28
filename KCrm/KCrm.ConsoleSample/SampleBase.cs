
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KCrm.ConsoleSample {
    public abstract class SampleBase<T> : IAsyncDisposable where T : DbContext {
        protected static readonly Random _random = new Random ( );
        protected T Context { get; set; }
        protected ILogger _logger { get; set; }

        public async Task RunAsync<R>(IConfiguration configuration, ILogger logger) where R : DbContext {
            SetupContext<R> (configuration, logger);
            await Context!.Database.MigrateAsync ( );

            await SeedData ( );
            await Context.SaveChangesAsync ( );
        }
        protected abstract Task SeedData();

        public async Task EnsureDeleted<R>(IConfiguration configuration, ILogger logger) where R : DbContext {
            SetupContext<R> (configuration, logger);

            await Context.Database.EnsureDeletedAsync ( );
        }

        private void SetupContext<R>(IConfiguration configuration, ILogger logger) where R : DbContext {
            _logger = logger;
            var connectionString = configuration[$"ConnectionStrings:AppConnection"] ?? string.Empty;
            _logger.LogInformation ($"App connection: {connectionString}");
            var builder = new DbContextOptionsBuilder<R> ( ).UseNpgsql (connectionString)
                .UseSnakeCaseNamingConvention ( );
            Context = Activator.CreateInstance (typeof (R), builder.Options) as T;
        }

        public ValueTask DisposeAsync() {
            Context?.Dispose ( );
            return default;
        }
    }
}
