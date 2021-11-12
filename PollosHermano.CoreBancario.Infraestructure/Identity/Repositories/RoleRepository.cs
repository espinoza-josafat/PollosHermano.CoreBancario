using PollosHermano.MicroFramework.Infraestructure.Repositories.Common;
using PollosHermano.CoreBancario.Domian.Identity.Repositories;
using PollosHermano.CoreBancario.Entities.Identity;
using PollosHermano.CoreBancario.Infraestructure.Identity.Factories;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Identity.Repositories
{
    public class RoleRepository : RepositoryGenericSqlServer<Role>, IRoleRepository
    {
        public RoleRepository(IIdentityFactory dbFactory)
            : base(dbFactory)
        {
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }
    }
}
