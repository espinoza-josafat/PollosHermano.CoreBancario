using PollosHermano.CoreBancario.Entities.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Dao
{
    public interface ICatTipoCuentaDao
    {
        Task<IEnumerable<GetCatTipoCuentaListModel>> GetCatTipoCuentaListAsync();
    }
}
