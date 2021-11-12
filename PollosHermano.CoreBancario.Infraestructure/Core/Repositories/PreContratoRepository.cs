using PollosHermano.MicroFramework.Infraestructure.Repositories.Common;
using PollosHermano.CoreBancario.Domian.Core.Repositories;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Infraestructure.Core.Factories;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Repositories
{
    public class PreContratoRepository : RepositorySqlServer<PreContrato>, IPreContratoRepository
    {
        public PreContratoRepository(IPollosHermanoCoreBancarioDBFactory dbFactory)
            : base(dbFactory)
        {
        
        }
        
        public async Task<PreContrato> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }
        
    }
}
