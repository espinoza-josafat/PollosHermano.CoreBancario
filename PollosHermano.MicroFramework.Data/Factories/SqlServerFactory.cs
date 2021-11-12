using PollosHermano.MicroFramework.Data.Interfaces;

namespace PollosHermano.MicroFramework.Data.Factories
{
    class SqlServerFactory : GenericDataBaseFactory
    {
        public override IDataBase GetDataBase(string connectionString)
        {
            return new SqlServer(connectionString);
        }
    }
}
