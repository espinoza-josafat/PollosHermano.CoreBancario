using System;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //Commit sobre la base de datos. Si hay un problema de concurrencia lanzará una excepción
        void Commit();

        //Commit sobre la base de datos. Si hay un problema de concurrencia lanzará una excepción
        Task CommitAsync();
    }
}
