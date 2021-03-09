using System.Threading.Tasks;

namespace Sample.Demo.Data
{
    public interface IDemoDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
