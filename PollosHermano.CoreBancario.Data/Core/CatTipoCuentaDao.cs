using Dapper;
using PollosHermano.CoreBancario.Common;
using PollosHermano.CoreBancario.Domian.Core.Dao;
using PollosHermano.CoreBancario.Entities.Core.Models;
using PollosHermano.MicroFramework.Data.Enums;
using PollosHermano.MicroFramework.Data.Factories;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Data.Core
{
    public class CatTipoCuentaDao : ICatTipoCuentaDao
    {
        public async Task<IEnumerable<GetCatTipoCuentaListModel>> GetCatTipoCuentaListAsync()
        {
            using var dataBase = DataBaseFactory.InitializeFactories().ExecuteCreation(DataBaseType.SqlServer, ConnectionStrings.PollosHermanoCoreBancarioDBConnectionString);
            return await dataBase.Connection.QueryAsync<GetCatTipoCuentaListModel>("[dbo].[cspGetCatTipoCuentaList]", commandType: CommandType.StoredProcedure);
        }
    }
}
