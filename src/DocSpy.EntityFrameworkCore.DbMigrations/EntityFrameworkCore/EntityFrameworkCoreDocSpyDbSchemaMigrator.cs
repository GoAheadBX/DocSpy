using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DocSpy.Data;
using Volo.Abp.DependencyInjection;

namespace DocSpy.EntityFrameworkCore
{
    public class EntityFrameworkCoreDocSpyDbSchemaMigrator
        : IDocSpyDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreDocSpyDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the DocSpyMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<DocSpyMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}