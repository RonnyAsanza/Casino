using Casino.Domain.Entities;
using Casino.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Casino.Infrastructure.Migrations.Migrations
{
    public static class CasinoDbMigrator
    {
        public static void SeedTranslationsData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<CasinoDbContext>();
            context.Database.Migrate();

            var assembly = typeof(CasinoDbMigrator).Assembly;
            var files = assembly.GetManifestResourceNames();

            var executedSeedings = context.SeedingEntries.ToArray();
            var filePrefix = $"{assembly.GetName().Name}.Seedings.";
            foreach (var file in files.Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                                      .Select(f => new
                                      {
                                          PhysicalFile = f,
                                          LogicalFile = f.Replace(filePrefix, string.Empty)
                                      })
                                      .OrderBy(f => f.LogicalFile))
            {
                if (executedSeedings.Any(e => e.Name == file.LogicalFile))
                    continue;

                string command = string.Empty;
                using (Stream stream = assembly.GetManifestResourceStream(file.PhysicalFile))
                {
                    using StreamReader reader = new(stream);
                    command = reader.ReadToEnd();
                }

                if (string.IsNullOrWhiteSpace(command))
                    continue;

                using var transaction = context.Database.BeginTransaction();
                try
                {
                    context.Database.ExecuteSqlRaw(command);
                    context.SeedingEntries.Add(new SeedingEntry() { Name = file.LogicalFile });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }

            }
        }

    }
}
