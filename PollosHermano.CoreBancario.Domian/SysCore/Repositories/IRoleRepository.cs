using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.CoreBancario.Entities.SysCore;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.SysCore.Repositories
{
    public partial interface IRoleRepository : IRepositorySqlServer<Role>
    {
        Task<Role> GetByIdAsync(Guid id);
    }
}
