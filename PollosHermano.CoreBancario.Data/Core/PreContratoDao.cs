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
    public class PreContratoDao : IPreContratoDao
    {
        public async Task<IEnumerable<GetPreContratoListModel>> GetPreContratoListAsync()
        {
            using var dataBase = DataBaseFactory.InitializeFactories().ExecuteCreation(DataBaseType.SqlServer, ConnectionStrings.PollosHermanoCoreBancarioDBConnectionString);
            return await dataBase.Connection.QueryAsync<GetPreContratoListModel>("[dbo].[cspGetPreContratoList]", commandType: CommandType.StoredProcedure);
        }
    }
}
