namespace PollosHermano.CoreBancario.Entities.Catalogs.Models
{
    public class PlugInConfiguration : CastModelPlugIn
    {
        public string AssemblyPath { get; set; }

        public bool IsAssemblyPathRelative { get; set; } = true;

        public CastModelPlugIn CastModel { get; set; } = null;
    }
}
