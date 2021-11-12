using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.CoreBancario.Entities.Core;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Repositories
{
    public interface ICatTipoCuentaRepository : IRepositorySqlServer<CatTipoCuenta>
    {
        Task<CatTipoCuenta> GetByIdAsync(byte id);
    
    }
}
