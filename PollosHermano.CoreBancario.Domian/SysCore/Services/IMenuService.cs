using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Entities.SysCore;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.SysCore.Services
{
    public interface IMenuService : IEntityService<Menu>
    {
        Task<Menu> GetByIdAsync(Guid id);
    }
}
