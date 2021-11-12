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
    public class ContratoService : EntityService<Contrato>, IContratoService
    {
        readonly IPollosHermanoCoreBancarioDBUnitOfWork _unitOfWork;
        readonly IContratoRepository _repository;
        readonly IContratoDao _dao;

        public ContratoService(IPollosHermanoCoreBancarioDBUnitOfWork unitOfWork, IContratoRepository repository, IContratoDao dao)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _dao = dao;
        }

        public async Task<IEnumerable<GetContratoListModel>> GetContratoListAsync()
        {
            return await _dao.GetContratoListAsync();
        }
        
        public async Task<Contrato> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        
    }
}
