using PollosHermano.MicroFramework.Infraestructure.Repositories.Common;
using PollosHermano.CoreBancario.Domian.Core.Repositories;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Infraestructure.Core.Factories;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Repositories
{
    public partial class CatTipoCuentaRepository : RepositorySqlServer<CatTipoCuenta>, ICatTipoCuentaRepository
    {
        public CatTipoCuentaRepository(IPollosHermanoCoreBancarioDBFactory dbFactory)
            : base(dbFactory)
        {
        
        }
        
        public async Task<CatTipoCuenta> GetByIdAsync(byte id)
        {
            return await base.GetByIdAsync(id);
        }
        
    }
}
