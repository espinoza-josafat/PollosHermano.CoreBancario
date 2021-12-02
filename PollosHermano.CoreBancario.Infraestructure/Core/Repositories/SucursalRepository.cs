using PollosHermano.MicroFramework.Infraestructure.Repositories.Common;
using PollosHermano.CoreBancario.Domian.Core.Repositories;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Infraestructure.Core.Factories;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Repositories
{
    public partial class SucursalRepository : RepositorySqlServer<Sucursal>, ISucursalRepository
    {
        public SucursalRepository(IPollosHermanoCoreBancarioDBFactory dbFactory)
            : base(dbFactory)
        {
        
        }
        
        public async Task<Sucursal> GetByIdAsync(byte id)
        {
            return await base.GetByIdAsync(id);
        }
        
    }
}
