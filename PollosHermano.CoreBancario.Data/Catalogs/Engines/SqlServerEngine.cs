using PollosHermano.CoreBancario.Data.Catalogs.Engines.Contracts;
using PollosHermano.CoreBancario.Entities.Catalogs.Enums;
using PollosHermano.CoreBancario.Entities.Catalogs.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TB.ComponentModel;

namespace PollosHermano.CoreBancario.Data.Catalogs.Engines
{
    public class SqlServerEngine : IDataBaseEngine
    {
        public async Task<object> ExecuteAsync(DataBaseEngineConfiguration configuration)
        {
            var data = new List<List<dynamic>>();

            var connectionString = configuration.ConnectionString;

            using var connection = new SqlConnection(connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = configuration.Query;

            if (configuration.Type == TypeDatabaseExecution.StoredProcedure)
                command.CommandType = CommandType.StoredProcedure;

            if (configuration.Parameters?.Count > 0)
                for (int i = 0; i < configuration.Parameters.Count; i++)
                {
                    var parameter = configuration.Parameters[i];
                    var parameterName = parameter.Key;
                    var sValue = parameter.Value;
                    if (sValue == null)
                        command.Parameters.Add(new SqlParameter(parameterName, DBNull.Value));
                    else
                    {
                        var dataTypeName = parameter.Type.ToString().Replace('_', '.');
                        var type = Type.GetType(dataTypeName);
                        var value = sValue.To(type);
                        command.Parameters.Add(new SqlParameter(parameterName, value));
                    }
                }

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            do
            {
                data.Add((List<dynamic>)Helpers.DataReaderMapper.MapToListDynamic(reader));
            }
            while (reader.NextResult());

            if (data.Count == 1)
            {
                return data[0];
            }
            else
            {
                var result = new Dictionary<string, List<dynamic>>();

                for (var i = 0; i < data.Count; i++)
                {
                    result.Add($"DataSource_{i + 1}", data[i]);
                }

                return result;
            }
        }
    }
}
