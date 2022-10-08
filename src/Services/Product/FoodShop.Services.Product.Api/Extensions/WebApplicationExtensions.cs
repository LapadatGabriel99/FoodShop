using FoodShop.Services.Product.Api.Services.Contracts.Seeder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodShop.Services.Product.Api.Extensions
{
    internal static class WebApplicationExtensions
    {
        public static WebApplication MigrateDatabase<TContext>(this WebApplication app, int? retry = 0)
           where TContext : DbContext
        {
            var retryForAvailability = retry.Value;

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
                var context = serviceProvider.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(context, serviceProvider);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(app, retryForAvailability);
                    }
                }
            }

            return app;
        }

        private static void InvokeSeeder<TContext>(TContext context, IServiceProvider serviceProvider)
            where TContext : DbContext
        {
            context.Database.Migrate();

            var dbSeeder = serviceProvider.GetRequiredService<IDbSeederService>();
            dbSeeder.SeedDatabaseAsync().GetAwaiter().GetResult();
        }
    }
}
