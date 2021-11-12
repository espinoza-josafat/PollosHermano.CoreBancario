using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Domain.Repositories
{
    public interface IRepositoryBase<TEntity>
    {
        IEnumerable<TEntity> Get();

        Task<IEnumerable<TEntity>> GetAsync();

        TEntity GetById(object id);

        Task<TEntity> GetByIdAsync(object id);

        TEntity Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        void Delete(object id);

        Task DeleteAsync(object id);

        Task DeleteAsync(TEntity entity);
    }
}
