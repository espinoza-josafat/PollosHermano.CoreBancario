using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.CoreBancario.Entities.Core;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Repositories
{
    public partial interface IContratoRepository : IRepositorySqlServer<Contrato>
    {
        Task<Contrato> GetByIdAsync(int id);
    
    }
}
