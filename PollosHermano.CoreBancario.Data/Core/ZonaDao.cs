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
    public partial class ZonaDao : IZonaDao
    {
        public async Task<IEnumerable<GetZonaListModel>> GetZonaListAsync()
        {
            using var dataBase = DataBaseFactory.InitializeFactories().ExecuteCreation(DataBaseType.SqlServer, ConnectionStrings.PollosHermanoCoreBancarioDBConnectionString);
            return await dataBase.Connection.QueryAsync<GetZonaListModel>("[dbo].[cspGetZonaList]", commandType: CommandType.StoredProcedure);
        }
    }
}
