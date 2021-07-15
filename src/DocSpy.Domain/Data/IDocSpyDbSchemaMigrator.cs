using System.Threading.Tasks;

namespace DocSpy.Data
{
    public interface IDocSpyDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
