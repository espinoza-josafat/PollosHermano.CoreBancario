using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.CoreBancario.Entities.Core;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Repositories
{
    public partial interface ISucursalRepository : IRepositorySqlServer<Sucursal>
    {
        Task<Sucursal> GetByIdAsync(byte id);
    
    }
}
