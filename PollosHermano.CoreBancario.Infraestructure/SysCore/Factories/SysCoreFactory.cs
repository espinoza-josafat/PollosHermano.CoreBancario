using PollosHermano.MicroFramework.Infraestructure;
using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using PollosHermano.CoreBancario.Infraestructure.SysCore.DbContexts;

namespace PollosHermano.CoreBancario.Infraestructure.SysCore.Factories
{
    public class SysCoreFactory : Disposable, ISysCoreFactory
    {
        IUnitOfWorkSqlServer _unitOfWork;

        public IUnitOfWorkSqlServer Init()
        {
            if (_unitOfWork == null)
                _unitOfWork = new SysCoreContext();

            return _unitOfWork;
        }

        protected override void DisposeCore()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }
    }
}
