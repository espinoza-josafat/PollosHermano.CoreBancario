using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using PollosHermano.CoreBancario.Domian.SysCore.UnitOfWork;
using PollosHermano.CoreBancario.Infraestructure.SysCore.Factories;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.SysCore.UnitOfWork
{
    public class SysCoreUnitOfWork : ISysCoreUnitOfWork
    {
        readonly IUnitOfWorkSqlServer _unitOfWork;

        public SysCoreUnitOfWork(ISysCoreFactory dbFactory)
        {
            if (dbFactory == null)
                throw new ArgumentNullException(nameof(dbFactory));

            _unitOfWork = dbFactory.Init();
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public async Task CommitAsync()
        {
            await _unitOfWork.CommitAsync();
        }

        public virtual void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
