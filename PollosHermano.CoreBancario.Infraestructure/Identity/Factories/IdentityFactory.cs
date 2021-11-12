using PollosHermano.MicroFramework.Infraestructure;
using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using PollosHermano.CoreBancario.Infraestructure.Identity.DbContexts;

namespace PollosHermano.CoreBancario.Infraestructure.Identity.Factories
{
    public class IdentityFactory : Disposable, IIdentityFactory
    {
        IUnitOfWorkSqlServer _unitOfWork;

        public IUnitOfWorkSqlServer Init()
        {
            if (_unitOfWork == null)
                _unitOfWork = new IdentityContext();

            return _unitOfWork;
        }

        protected override void DisposeCore()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }
    }
}
