using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Domian.Identity.Repositories;
using PollosHermano.CoreBancario.Domian.Identity.Services;
using PollosHermano.CoreBancario.Domian.Identity.UnitOfWork;
using PollosHermano.CoreBancario.Entities.Identity;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Application.Identity.Services
{
    public class UserService : EntityService<User>, IUserService
    {
        readonly IIdentityUnitOfWork _unitOfWork;
        readonly IUserRepository _repository;

        public UserService(IIdentityUnitOfWork unitOfWork, IUserRepository repository)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
