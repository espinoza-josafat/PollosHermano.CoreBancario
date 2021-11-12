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
    public class ClienteService : EntityService<Cliente>, IClienteService
    {
        readonly IPollosHermanoCoreBancarioDBUnitOfWork _unitOfWork;
        readonly IClienteRepository _repository;
        readonly IClienteDao _dao;

        public ClienteService(IPollosHermanoCoreBancarioDBUnitOfWork unitOfWork, IClienteRepository repository, IClienteDao dao)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _dao = dao;
        }

        public async Task<IEnumerable<GetClienteListModel>> GetClienteListAsync()
        {
            return await _dao.GetClienteListAsync();
        }
        
        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        
    }
}
