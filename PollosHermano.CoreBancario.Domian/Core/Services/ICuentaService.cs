using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Entities.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Services
{
    public interface ICuentaService : IEntityService<Cuenta>
    {
        Task<IEnumerable<GetCuentaListModel>> GetCuentaListAsync();

        Task<Cuenta> GetByIdAsync(int id);
    
    }
}
