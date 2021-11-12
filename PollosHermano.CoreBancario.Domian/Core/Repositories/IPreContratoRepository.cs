using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.CoreBancario.Entities.Core;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Repositories
{
    public interface IPreContratoRepository : IRepositorySqlServer<PreContrato>
    {
        Task<PreContrato> GetByIdAsync(int id);
    
    }
}
