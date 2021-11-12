using PollosHermano.MicroFramework.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace PollosHermano.MicroFramework.Infraestructure.UnitOfWork
{
    public interface IUnitOfWorkSqlServer : IUnitOfWork
    {
        //Commit sobre la base de datos. Si hay un problema de concurrencia "refrescará" los datos del cliente. Aproximación "Client wins"
        void CommitAndRefreshChanges();

        //Rollback de los cambios que se han producido en la Unit of Work y que están siendo observados por ella
        void Rollback();



        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

        void SetModified<TEntity>(TEntity entity) where TEntity : class;

        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);

        int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}
