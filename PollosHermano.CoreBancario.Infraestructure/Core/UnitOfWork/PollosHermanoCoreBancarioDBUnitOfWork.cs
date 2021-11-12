using PollosHermano.MicroFramework.Infraestructure.UnitOfWork;
using PollosHermano.CoreBancario.Domian.Core.UnitOfWork;
using PollosHermano.CoreBancario.Infraestructure.Core.Factories;
using System;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Infraestructure.Core.UnitOfWork
{
    public class PollosHermanoCoreBancarioDBUnitOfWork : IPollosHermanoCoreBancarioDBUnitOfWork
    {
        readonly IUnitOfWorkSqlServer _unitOfWork;

        public PollosHermanoCoreBancarioDBUnitOfWork(IPollosHermanoCoreBancarioDBFactory dbFactory)
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
