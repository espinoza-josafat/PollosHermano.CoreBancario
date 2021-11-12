using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Application.Catalogs.Contracts
{
    public interface ICommandBase
    {
        Task<object> ExecuteAsync(object parameter);
    }
}
