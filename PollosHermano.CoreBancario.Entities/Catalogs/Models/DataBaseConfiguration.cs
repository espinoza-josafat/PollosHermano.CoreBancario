using PollosHermano.CoreBancario.Entities.Catalogs.Enums;

namespace PollosHermano.CoreBancario.Entities.Catalogs.Models
{
    public class DataBaseConfiguration : DataBaseEngineConfiguration
    {
        public string ConnectionStringName { get; set; } = null;

        public DatabaseEngine DatabaseEngine { get; set; } = DatabaseEngine.SqlServer;
    }
}
