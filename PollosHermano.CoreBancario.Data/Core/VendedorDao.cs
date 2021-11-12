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
    public class VendedorDao : IVendedorDao
    {
        public async Task<IEnumerable<GetVendedorListModel>> GetVendedorListAsync()
        {
            using var dataBase = DataBaseFactory.InitializeFactories().ExecuteCreation(DataBaseType.SqlServer, ConnectionStrings.PollosHermanoCoreBancarioDBConnectionString);
            return await dataBase.Connection.QueryAsync<GetVendedorListModel>("[dbo].[cspGetVendedorList]", commandType: CommandType.StoredProcedure);
        }
    }
}
