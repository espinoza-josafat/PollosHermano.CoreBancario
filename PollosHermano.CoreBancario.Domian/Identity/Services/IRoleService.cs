using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Entities.Identity;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Identity.Services
{
    public interface IRoleService : IEntityService<Role>
    {
        Task<Role> GetByIdAsync(Guid id);
    }
}
