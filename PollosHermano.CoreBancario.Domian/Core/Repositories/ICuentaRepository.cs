using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.CoreBancario.Entities.Core;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Repositories
{
    public interface ICuentaRepository : IRepositorySqlServer<Cuenta>
    {
        Task<Cuenta> GetByIdAsync(int id);
    
    }
}
