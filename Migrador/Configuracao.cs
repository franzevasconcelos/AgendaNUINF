using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaNUINF.Migrador {
    public class Configuracao {
        private static ServiceProvider InstanciaServiceProvider(string caminhoBanco) {
            return new ServiceCollection().AddFluentMigratorCore()
                                          .ConfigureRunner(rb => rb
                                                                 .AddSQLite()
                                                                 .WithGlobalConnectionString($"Data Source={caminhoBanco}")
                                                                 .ScanIn(typeof(Migracao01).Assembly)
                                                                 .For.Migrations())
                                          .AddLogging(lb => lb.AddFluentMigratorConsole())
                                          .BuildServiceProvider(false);
        }

        public static void RealizarUpgrade(string caminhoBanco) {
            var serviceProvider = InstanciaServiceProvider(caminhoBanco);

            using (var scope = serviceProvider.CreateScope()) {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }
    }
}