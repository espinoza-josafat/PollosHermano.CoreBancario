using PollosHermano.MicroFramework.Data.Enums;
using PollosHermano.MicroFramework.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace PollosHermano.MicroFramework.Data.Factories
{
    public class DataBaseFactory
    {
        readonly Dictionary<DataBaseType, GenericDataBaseFactory> _factories;

        DataBaseFactory()
        {
            _factories = new Dictionary<DataBaseType, GenericDataBaseFactory>();

            foreach (DataBaseType dataBaseType in Enum.GetValues(typeof(DataBaseType)))
            {
                var factory = (GenericDataBaseFactory)Activator.CreateInstance(Type.GetType("PollosHermano.MicroFramework.Data.Factories." + Enum.GetName(typeof(DataBaseType), dataBaseType) + "Factory"));
                _factories.Add(dataBaseType, factory);
            }
        }

        public static DataBaseFactory InitializeFactories() => new DataBaseFactory();

        public IDataBase ExecuteCreation(DataBaseType dataBaseType, string connectionString) => _factories[dataBaseType].GetDataBase(connectionString);
    }
}
