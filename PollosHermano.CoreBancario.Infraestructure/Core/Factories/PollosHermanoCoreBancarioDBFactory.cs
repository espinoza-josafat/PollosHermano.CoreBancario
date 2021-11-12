using PollosHermano.MicroFramework.Infraestructure;
using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using PollosHermano.CoreBancario.Infraestructure.Core.DbContexts;

namespace PollosHermano.CoreBancario.Infraestructure.Core.Factories
{
    public class PollosHermanoCoreBancarioDBFactory : Disposable, IPollosHermanoCoreBancarioDBFactory
    {
        IUnitOfWorkSqlServer _unitOfWork;

        public IUnitOfWorkSqlServer Init()
        {
            if (_unitOfWork == null)
                _unitOfWork = new PollosHermanoCoreBancarioDBContext();

            return _unitOfWork;
        }

        protected override void DisposeCore()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }
    }
}
