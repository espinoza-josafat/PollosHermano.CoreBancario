using PollosHermano.MicroFramework.Infraestructure.Repositories.Common;
using PollosHermano.CoreBancario.Domian.Identity.Repositories;
using PollosHermano.CoreBancario.Entities.Identity;
using PollosHermano.CoreBancario.Infraestructure.Identity.Factories;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Identity.Repositories
{
    public class UserRepository : RepositoryGenericSqlServer<User>, IUserRepository
    {
        public UserRepository(IIdentityFactory dbFactory)
            : base(dbFactory)
        {
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }
    }
}
