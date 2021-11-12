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
    public class SucursalService : EntityService<Sucursal>, ISucursalService
    {
        readonly IPollosHermanoCoreBancarioDBUnitOfWork _unitOfWork;
        readonly ISucursalRepository _repository;
        readonly ISucursalDao _dao;

        public SucursalService(IPollosHermanoCoreBancarioDBUnitOfWork unitOfWork, ISucursalRepository repository, ISucursalDao dao)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _dao = dao;
        }

        public async Task<IEnumerable<GetSucursalListModel>> GetSucursalListAsync()
        {
            return await _dao.GetSucursalListAsync();
        }
        
        public async Task<Sucursal> GetByIdAsync(byte id)
        {
            return await _repository.GetByIdAsync(id);
        }
        
    }
}
