using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using System;

namespace PollosHermano.MicroFramework.Infraestructure.Factories
{
    public interface IUnitOfWorkMongoDbFactory : IDisposable
    {
        IUnitOfWorkMongoDb Init();
    }
}
