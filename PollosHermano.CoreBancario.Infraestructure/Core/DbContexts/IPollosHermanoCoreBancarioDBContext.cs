using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using PollosHermano.CoreBancario.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace PollosHermano.CoreBancario.Infraestructure.Core.DbContexts
{
    public partial interface IPollosHermanoCoreBancarioDBContext : IUnitOfWorkSqlServer
    {
        DbSet<Sucursal> Sucursal { get; set; }
        DbSet<Zona> Zona { get; set; }
        DbSet<Vendedor> Vendedor { get; set; }
        DbSet<PreContrato> PreContrato { get; set; }
        DbSet<Contrato> Contrato { get; set; }
        DbSet<CatTipoCuenta> CatTipoCuenta { get; set; }
        DbSet<Cliente> Cliente { get; set; }
        DbSet<Cuenta> Cuenta { get; set; }
        
    }
}
