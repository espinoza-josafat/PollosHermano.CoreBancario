using PollosHermano.MicroFramework.Domain.Sevices;
using PollosHermano.CoreBancario.Domian.Core.Dao;
using PollosHermano.CoreBancario.Domian.Core.Repositories;
using PollosHermano.CoreBancario.Domian.Core.Services;
using PollosHermano.CoreBancario.Domian.Core.UnitOfWork;
using PollosHermano.CoreBancario.Entities.Core;
using PollosHermano.CoreBancario.Entities.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Application.Core.Services
{
    public class CuentaService : EntityService<Cuenta>, ICuentaService
    {
        readonly IPollosHermanoCoreBancarioDBUnitOfWork _unitOfWork;
        readonly ICuentaRepository _repository;
        readonly ICuentaDao _dao;

        public CuentaService(IPollosHermanoCoreBancarioDBUnitOfWork unitOfWork, ICuentaRepository repository, ICuentaDao dao)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _dao = dao;
        }

        public async Task<IEnumerable<GetCuentaListModel>> GetCuentaListAsync()
        {
            return await _dao.GetCuentaListAsync();
        }
        
        public async Task<Cuenta> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        
    }
}
