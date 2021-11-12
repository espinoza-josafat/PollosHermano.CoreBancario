using PollosHermano.MicroFramework.Domain.Repositories;
using PollosHermano.MicroFramework.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Domain.Sevices
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IRepositoryBase<TEntity> _repository;

        public EntityService(IUnitOfWork unitOfWork, IRepositoryBase<TEntity> repository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual void Create(TEntity entity)
        {
            ValidateEntity(entity);

            _repository.Insert(entity);
            _unitOfWork.Commit();
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            ValidateEntity(entity);

            await _repository.InsertAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public virtual void Update(TEntity entity)
        {
            ValidateEntity(entity);

            _repository.Update(entity);
            _unitOfWork.Commit();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            ValidateEntity(entity);

            await _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public virtual void Delete(TEntity entity)
        {
            ValidateEntity(entity);

            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            ValidateEntity(entity);

            await _repository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        void ValidateEntity(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _repository.Get();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _repository.GetAsync();
        }
    }
}
