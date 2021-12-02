using PollosHermano.MicroFramework.Data.Enums;
using PollosHermano.MicroFramework.Data.Factories;
using PollosHermano.CoreBancario.Common;
using PollosHermano.CoreBancario.Domian.SysCore.Dao;
using PollosHermano.CoreBancario.Entities.SysCore.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Data.SysCore
{
    public partial class MenuDao : IMenuDao
    {
        public async Task<IEnumerable<MenuModel>> GetMenusByUser(Guid userId)
        {
            using var dataBase = DataBaseFactory.InitializeFactories().ExecuteCreation(DataBaseType.SqlServer, ConnectionStrings.SysCoreConnectionString);
            return await dataBase.Connection.QueryAsync<MenuModel>($@"[syscore].[cspGetMenusByUser]", new { userId });
        }
    }
}
