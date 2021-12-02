using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Entities.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Services
{
    public partial interface IZonaService : IEntityService<Zona>
    {
        Task<IEnumerable<GetZonaListModel>> GetZonaListAsync();

        Task<Zona> GetByIdAsync(int id);
    
    }
}
