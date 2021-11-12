using PollosHermano.CoreBancario.Entities.Catalogs.Enums;

namespace PollosHermano.CoreBancario.Entities.Catalogs.Models
{
    public class DataBaseParameter : KeyValueItem
    {
        public PrimitiveDataType Type { get; set; } = PrimitiveDataType.System_String;
    }
}
