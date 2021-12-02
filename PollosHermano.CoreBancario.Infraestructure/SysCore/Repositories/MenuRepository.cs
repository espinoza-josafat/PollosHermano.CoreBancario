using PollosHermano.MicroFramework.Infraestructure.Repositories.Common;
using PollosHermano.CoreBancario.Domian.SysCore.Repositories;
using PollosHermano.CoreBancario.Entities.SysCore;
using PollosHermano.CoreBancario.Infraestructure.SysCore.Factories;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.SysCore.Repositories
{
    public class MenuRepository : RepositoryGenericSqlServer<Menu>, IMenuRepository
    {
        public MenuRepository(ISysCoreFactory dbFactory)
            : base(dbFactory)
        {
        }

        public async Task<Menu> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }
    }
}
