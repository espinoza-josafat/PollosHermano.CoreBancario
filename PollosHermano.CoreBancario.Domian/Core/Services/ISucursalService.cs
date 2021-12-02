using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Entities.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Services
{
    public partial interface ISucursalService : IEntityService<Sucursal>
    {
        Task<IEnumerable<GetSucursalListModel>> GetSucursalListAsync();

        Task<Sucursal> GetByIdAsync(byte id);
    
    }
}
