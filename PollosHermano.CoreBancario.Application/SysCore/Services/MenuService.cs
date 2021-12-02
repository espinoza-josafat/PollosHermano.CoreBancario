using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Domian.SysCore.Repositories;
using PollosHermano.CoreBancario.Domian.SysCore.Services;
using PollosHermano.CoreBancario.Domian.SysCore.UnitOfWork;
using PollosHermano.CoreBancario.Entities.SysCore;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Application.SysCore.Services
{
    public class MenuService : EntityService<Menu>, IMenuService
    {
        readonly ISysCoreUnitOfWork _unitOfWork;
        readonly IMenuRepository _repository;

        public MenuService(ISysCoreUnitOfWork unitOfWork, IMenuRepository repository)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Menu> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
