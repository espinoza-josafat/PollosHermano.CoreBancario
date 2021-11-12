using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PollosHermano.CoreBancario.Entities.Catalogs.Models
{
    public class ExecuteModel
    {
        public string CatalogId { get; set; }

        public Dictionary<string, string> Parameters { get; set; } = null;

        public JToken Request { get; set; } = null;
    }
}
