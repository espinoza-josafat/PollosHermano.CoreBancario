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
    public partial class PreContratoService : EntityService<PreContrato>, IPreContratoService
    {
        readonly IPollosHermanoCoreBancarioDBUnitOfWork _unitOfWork;
        readonly IPreContratoRepository _repository;
        readonly IPreContratoDao _dao;

        public PreContratoService(IPollosHermanoCoreBancarioDBUnitOfWork unitOfWork, IPreContratoRepository repository, IPreContratoDao dao)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _dao = dao;
        }

        public async Task<IEnumerable<GetPreContratoListModel>> GetPreContratoListAsync()
        {
            return await _dao.GetPreContratoListAsync();
        }
        
        public async Task<PreContrato> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        
    }
}
