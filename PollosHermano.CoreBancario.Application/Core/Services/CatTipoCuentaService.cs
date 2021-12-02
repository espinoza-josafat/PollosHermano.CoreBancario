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
    public partial class CatTipoCuentaService : EntityService<CatTipoCuenta>, ICatTipoCuentaService
    {
        readonly IPollosHermanoCoreBancarioDBUnitOfWork _unitOfWork;
        readonly ICatTipoCuentaRepository _repository;
        readonly ICatTipoCuentaDao _dao;

        public CatTipoCuentaService(IPollosHermanoCoreBancarioDBUnitOfWork unitOfWork, ICatTipoCuentaRepository repository, ICatTipoCuentaDao dao)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _dao = dao;
        }

        public async Task<IEnumerable<GetCatTipoCuentaListModel>> GetCatTipoCuentaListAsync()
        {
            return await _dao.GetCatTipoCuentaListAsync();
        }
        
        public async Task<CatTipoCuenta> GetByIdAsync(byte id)
        {
            return await _repository.GetByIdAsync(id);
        }
        
    }
}
