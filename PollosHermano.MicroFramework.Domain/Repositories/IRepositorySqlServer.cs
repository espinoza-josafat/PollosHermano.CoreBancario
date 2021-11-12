using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Domain.Repositories
{
    /// <summary>
    /// Métodos genéricos para todos los repositorios
    /// </summary>
    public interface IRepositorySqlServer<TEntity> : IRepositoryBase<TEntity>
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        void DeleteRange(params TEntity[] entitiesToDelete);

        void DeleteRange(IEnumerable<TEntity> entitiesToDelete);

        IEnumerable<TEntity> GetPagedElements<TKey>(int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true, string includeProperties = "");

        Task<IEnumerable<TEntity>> GetPagedElementsAsync<TKey>(int pageIndex, int pageCount, Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true, string includeProperties = "");

        IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters);

        Task<IEnumerable<TEntity>> GetFromDatabaseWithQueryAsync(string sqlQuery, params object[] parameters);

        int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters);

        Task<int> ExecuteInDatabaseByQueryAsync(string sqlCommand, params object[] parameters);
    }
}
