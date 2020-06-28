using System;
using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;

namespace KCrm.Aegis.Migrator {
    public class Migrator {

        private string _connectionString;

        public void UpdateDatabase(IConfiguration configuration) {
            _connectionString = configuration["ConnectionStrings:AegisConnection"];
            var upgradeEngineBuilder = DeployChanges.To
                .PostgresqlDatabase (_connectionString, "aegis")
                .WithScriptsEmbeddedInAssembly (Assembly.GetExecutingAssembly ( ))
                .WithTransaction ( )
                .LogToConsole ( );

            var upgradeEngine = upgradeEngineBuilder.Build ( );
            if (upgradeEngine.IsUpgradeRequired ( )) {
                var result = upgradeEngine.PerformUpgrade ( );
                Console.WriteLine ("Result: " + result.Successful);

                if (!result.Successful) {
                    Console.WriteLine ("Database update failed");
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine ("Database update successfully");
                    Console.ResetColor ( );
                }
            }
        }
    }
}
