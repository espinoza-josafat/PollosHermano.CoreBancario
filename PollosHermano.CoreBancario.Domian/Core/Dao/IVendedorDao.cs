using PollosHermano.CoreBancario.Entities.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Domian.Core.Dao
{
    public partial interface IVendedorDao
    {
        Task<IEnumerable<GetVendedorListModel>> GetVendedorListAsync();
    }
}
