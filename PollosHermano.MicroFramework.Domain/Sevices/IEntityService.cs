using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Domain.Sevices
{
    public interface IEntityService<TEntity> : IService
     where TEntity : class
    {
        void Create(TEntity entity);

        Task CreateAsync(TEntity entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        IEnumerable<TEntity> Get();

        Task<IEnumerable<TEntity>> GetAsync();
    }
}
