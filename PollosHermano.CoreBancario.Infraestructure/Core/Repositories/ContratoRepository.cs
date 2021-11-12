using PollosHermano.MicroFramework.Infraestructure.Repositories.Common;
using PollosHermano.CoreBancario.Domian.Core.Repositories;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Infraestructure.Core.Factories;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Repositories
{
    public class ContratoRepository : RepositorySqlServer<Contrato>, IContratoRepository
    {
        public ContratoRepository(IPollosHermanoCoreBancarioDBFactory dbFactory)
            : base(dbFactory)
        {
        
        }
        
        public async Task<Contrato> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }
        
    }
}
