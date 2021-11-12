using System;

namespace PollosHermano.MicroFramework.Tools.Factories
{
    public static class GenericFactory<T> where T : class, new()
    {
        public static T CreateInstance()
        {
            return new T();
        }

        public static TService GetService<TRepository, TService>() where TRepository : new()
        {
            var repository = new TRepository();
            return (TService)Activator.CreateInstance(typeof(TService), repository);
        }
    }
}
