using PollosHermano.CoreBancario.Entities.SysCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.SysCore.Dao
{
    public partial interface IMenuDao
    {
        Task<IEnumerable<MenuModel>> GetMenusByUser(Guid userId);
    }
}
