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
    public class ZonaService : EntityService<Zona>, IZonaService
    {
        readonly IPollosHermanoCoreBancarioDBUnitOfWork _unitOfWork;
        readonly IZonaRepository _repository;
        readonly IZonaDao _dao;

        public ZonaService(IPollosHermanoCoreBancarioDBUnitOfWork unitOfWork, IZonaRepository repository, IZonaDao dao)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _dao = dao;
        }

        public async Task<IEnumerable<GetZonaListModel>> GetZonaListAsync()
        {
            return await _dao.GetZonaListAsync();
        }
        
        public async Task<Zona> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        
    }
}
