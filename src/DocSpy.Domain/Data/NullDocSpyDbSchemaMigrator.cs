using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace DocSpy.Data
{
    /* This is used if database provider does't define
     * IDocSpyDbSchemaMigrator implementation.
     */
    public class NullDocSpyDbSchemaMigrator : IDocSpyDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}