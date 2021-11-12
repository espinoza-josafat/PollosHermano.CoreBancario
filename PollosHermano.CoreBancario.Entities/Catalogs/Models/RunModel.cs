using PollosHermano.CoreBancario.Entities.Catalogs.Enums;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PollosHermano.CoreBancario.Entities.Catalogs.Models
{
    public class RunModel
    {
        public RunCatalogModel Catalog { get; set; }

        public RunExecuteModel Run { get; set; }
    }

    public class RunCatalogModel
    {
        public CatalogType Type { get; set; } = CatalogType.Static;

        public object Configuration { get; set; }
    }

    public class RunExecuteModel
    {
        public Dictionary<string, string> Parameters { get; set; } = null;

        public JToken Request { get; set; } = null;
    }
}
