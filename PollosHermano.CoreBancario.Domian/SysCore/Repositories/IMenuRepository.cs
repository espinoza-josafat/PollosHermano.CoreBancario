using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.CoreBancario.Entities.SysCore;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.SysCore.Repositories
{
    public partial interface IMenuRepository : IRepositorySqlServer<Menu>
    {
        Task<Menu> GetByIdAsync(Guid id);
    }
}
