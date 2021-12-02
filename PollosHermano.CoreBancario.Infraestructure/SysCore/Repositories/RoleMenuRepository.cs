using PollosHermano.MicroFramework.Infraestructure.Repositories.Common;
using PollosHermano.CoreBancario.Domian.SysCore.Repositories;
using PollosHermano.CoreBancario.Entities.SysCore;
using PollosHermano.CoreBancario.Infraestructure.SysCore.Factories;

namespace PollosHermano.CoreBancario.Infraestructure.SysCore.Repositories
{
    public class RoleMenuRepository : RepositoryGenericSqlServer<RoleMenu>, IRoleMenuRepository
    {
        public RoleMenuRepository(ISysCoreFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
