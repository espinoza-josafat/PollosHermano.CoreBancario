using PollosHermano.CoreBancario.Entities.Catalogs.Enums;
using System.Collections.Generic;

namespace PollosHermano.CoreBancario.Entities.Catalogs.Models
{
    public class ApiConfiguration
    {
        public string Endpoint { get; set; }

        public TypeApi Type { get; set; } = TypeApi.Get;

        public Dictionary<string, string> Headers { get; set; } = null;

        public string Request { get; set; } = null;
    }
}
