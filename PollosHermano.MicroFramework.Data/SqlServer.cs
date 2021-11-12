using PollosHermano.MicroFramework.Data.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PollosHermano.MicroFramework.Data
{
    public class SqlServer : Database, IDataBase
    {
        SqlConnection _connection;
        readonly string _connectionString;

        public override string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public override IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public SqlServer(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            _connectionString = connectionString;

            CreateConnection();
        }

        public SqlServer(string connectionString, bool openConnection)
            : this(connectionString)
        {
            if (openConnection)
                OpenConnection();
        }

        public override void CreateConnection()
        {
            if (_connection == null)
                _connection = new SqlConnection(_connectionString);
        }

        public override void CreateOpenConnection()
        {
            CreateConnection();

            OpenConnection();
        }

        public override async Task CreateOpenConnectionAsync()
        {
            CreateConnection();

            await OpenConnectionAsync();
        }

        public void Open()
        {
            OpenConnection();
        }

        public async Task OpenAsync()
        {
            await OpenConnectionAsync();
        }

        public override void OpenConnection()
        {
            ValidateConnection(_connection);

            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        public override async Task OpenConnectionAsync()
        {
            ValidateConnection(_connection);

            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();
        }

        public void Close()
        {
            CloseConnection();
        }

        public override void CloseConnection()
        {
            ValidateConnection(_connection);

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        public override IDbCommand CreateCommand()
        {
            ValidateConnection(_connection);

            return _connection.CreateCommand();
        }

        public override IDbCommand CreateCommand(string commandText, CommandType commandType = CommandType.Text)
        {
            var command = CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            return command;
        }

        public override IDataParameter CreateParameter(string parameterName, object parameterValue)
        {
            ValidateParameter(parameterName, parameterValue);

            return new SqlParameter(parameterName, parameterValue);
        }

        public override void AddParameter(IDbCommand command, string parameterName, object parameterValue)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            ValidateParameter(parameterName, parameterValue);

            ((SqlCommand)command).Parameters.Add(new SqlParameter(parameterName, parameterValue));
        }

        public override IDbTransaction BeginTransaction()
        {
            ValidateConnection(_connection);

            if (_connection.State != ConnectionState.Open)
                throw new DataException("El \"State\" de la conexión no es: \"Open\"");

            return _connection.BeginTransaction();
        }

        public override void Dispose()
        {
            if (_connection != null)
            {
                CloseConnection();

                _connection.Dispose();
                _connection = null;
            }
        }

        void ValidateConnection(SqlConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
        }

        void ValidateParameter(string parameterName, object parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentNullException("parameterName");
            if (parameterValue == null)
                throw new ArgumentNullException("parameterValue");
        }
    }
}
