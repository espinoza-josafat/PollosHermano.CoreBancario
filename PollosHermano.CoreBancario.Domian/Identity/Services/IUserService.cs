using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Entities.Identity;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Identity.Services
{
    public interface IUserService : IEntityService<User>
    {
        Task<User> GetByIdAsync(Guid id);
    }
}
