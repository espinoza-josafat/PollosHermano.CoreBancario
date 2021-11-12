using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Entities.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Services
{
    public interface ICatTipoCuentaService : IEntityService<CatTipoCuenta>
    {
        Task<IEnumerable<GetCatTipoCuentaListModel>> GetCatTipoCuentaListAsync();

        Task<CatTipoCuenta> GetByIdAsync(byte id);
    
    }
}
