using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Entities.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Services
{
    public partial interface IContratoService : IEntityService<Contrato>
    {
        Task<IEnumerable<GetContratoListModel>> GetContratoListAsync();

        Task<Contrato> GetByIdAsync(int id);
    
    }
}
