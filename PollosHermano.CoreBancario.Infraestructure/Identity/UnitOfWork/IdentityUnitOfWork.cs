using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using PollosHermano.CoreBancario.Domian.Identity.UnitOfWork;
using PollosHermano.CoreBancario.Infraestructure.Identity.Factories;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Identity.UnitOfWork
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        readonly IUnitOfWorkSqlServer _unitOfWork;

        public IdentityUnitOfWork(IIdentityFactory dbFactory)
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
