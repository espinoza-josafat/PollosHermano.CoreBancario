using PollosHermano.CoreBancario.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace PollosHermano.CoreBancario.Infraestructure.Core.DbContexts
{
    public partial class PollosHermanoCoreBancarioDBContext
    {
        public DbSet<Sucursal> Sucursal { get; set; }
        public DbSet<Zona> Zona { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<PreContrato> PreContrato { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<CatTipoCuenta> CatTipoCuenta { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Cuenta> Cuenta { get; set; }
        
    }
}
