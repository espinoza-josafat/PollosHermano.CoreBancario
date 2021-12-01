using PollosHermano.CoreBancario.Entities.SysCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.SysCore.Processes
{
    public interface IProcessGetMenusByUser
    {
        Task<List<MenuModel>> ExecuteAsync(Guid userId);
    }
}
