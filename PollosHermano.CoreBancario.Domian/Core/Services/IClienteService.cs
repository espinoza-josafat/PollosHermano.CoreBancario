using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Entities.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Services
{
    public interface IClienteService : IEntityService<Cliente>
    {
        Task<IEnumerable<GetClienteListModel>> GetClienteListAsync();

        Task<Cliente> GetByIdAsync(int id);
    
    }
}
