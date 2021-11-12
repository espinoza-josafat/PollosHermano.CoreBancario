using PollosHermano.CoreBancario.Entities.Catalogs.Enums;
using System.Collections.Generic;

namespace PollosHermano.CoreBancario.Entities.Catalogs.Models
{
    public class DataBaseEngineConfiguration
    {
        public string ConnectionString { get; set; } = null;

        public TypeDatabaseExecution Type { get; set; } = TypeDatabaseExecution.Query;

        public string Query { get; set; }

        public List<DataBaseParameter> Parameters { get; set; } = null;
    }
}
