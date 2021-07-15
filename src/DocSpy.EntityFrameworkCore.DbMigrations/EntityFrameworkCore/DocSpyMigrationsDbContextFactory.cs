using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Volo.Abp.EntityFrameworkCore.PostgreSql;

namespace DocSpy.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class DocSpyMigrationsDbContextFactory : IDesignTimeDbContextFactory<DocSpyMigrationsDbContext>
    {
        public DocSpyMigrationsDbContext CreateDbContext(string[] args)
        {
            DocSpyEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<DocSpyMigrationsDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"),x => x.UseNetTopologySuite());

            return new DocSpyMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../DocSpy.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
