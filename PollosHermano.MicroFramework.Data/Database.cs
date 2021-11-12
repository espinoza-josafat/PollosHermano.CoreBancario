using PollosHermano.MicroFramework.Data.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Data
{
    public abstract class Database : IDataBase
    {
        public abstract string ConnectionString { get; }

        public abstract IDbConnection Connection { get; }

        public abstract void CreateConnection();

        public abstract void CreateOpenConnection();

        public abstract Task CreateOpenConnectionAsync();

        public abstract void OpenConnection();

        public abstract Task OpenConnectionAsync();

        public abstract void CloseConnection();

        public abstract IDbCommand CreateCommand();

        public abstract IDbCommand CreateCommand(string commandText, CommandType commandType = CommandType.Text);

        public abstract IDataParameter CreateParameter(string parameterName, object parameterValue);

        public abstract void AddParameter(IDbCommand command, string parameterName, object parameterValue);

        public abstract IDbTransaction BeginTransaction();

        public abstract void Dispose();
    }
}
