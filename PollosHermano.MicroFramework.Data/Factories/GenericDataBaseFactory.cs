using PollosHermano.MicroFramework.Data.Interfaces;

namespace PollosHermano.MicroFramework.Data.Factories
{
    abstract class GenericDataBaseFactory
    {
        public abstract IDataBase GetDataBase(string connectionString);
    }
}
