using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.CoreBancario.Entities.Identity;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Identity.Repositories
{
    public interface IRoleRepository : IRepositorySqlServer<Role>
    {
        Task<Role> GetByIdAsync(Guid id);
    }
}
