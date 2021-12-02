using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Domian.SysCore.Repositories;
using PollosHermano.CoreBancario.Domian.SysCore.Services;
using PollosHermano.CoreBancario.Domian.SysCore.UnitOfWork;
using PollosHermano.CoreBancario.Entities.SysCore;

namespace PollosHermano.CoreBancario.Application.SysCore.Services
{
    public partial class RoleMenuService : EntityService<RoleMenu>, IRoleMenuService
    {
        readonly ISysCoreUnitOfWork _unitOfWork;
        readonly IRoleMenuRepository _repository;

        public RoleMenuService(ISysCoreUnitOfWork unitOfWork, IRoleMenuRepository repository)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
    }
}
