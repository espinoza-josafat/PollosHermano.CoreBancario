using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Domian.SysCore.Repositories;
using PollosHermano.CoreBancario.Domian.SysCore.Services;
using PollosHermano.CoreBancario.Domian.SysCore.UnitOfWork;
using PollosHermano.CoreBancario.Entities.SysCore;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Application.SysCore.Services
{
    public partial class UserService : EntityService<User>, IUserService
    {
        readonly ISysCoreUnitOfWork _unitOfWork;
        readonly IUserRepository _repository;

        public UserService(ISysCoreUnitOfWork unitOfWork, IUserRepository repository)
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
