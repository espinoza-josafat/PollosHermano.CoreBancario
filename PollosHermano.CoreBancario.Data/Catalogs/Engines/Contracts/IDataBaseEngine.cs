using PollosHermano.CoreBancario.Entities.Catalogs.Models;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Data.Catalogs.Engines.Contracts
{
    public interface IDataBaseEngine
    {
        Task<object> ExecuteAsync(DataBaseEngineConfiguration configuration);
    }
}
