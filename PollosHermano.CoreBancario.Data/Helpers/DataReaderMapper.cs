using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;

namespace PollosHermano.CoreBancario.Data.Helpers
{
    public class DataReaderMapper
    {
        public static IList<dynamic> MapToListDynamic(DbDataReader dataReader)
        {
            if (dataReader == null)
                throw new ArgumentNullException("reader");

            var result = new List<dynamic>();

            if (dataReader.HasRows)
            {
                HashSet<string> fieldsDataReader = null;

                while (dataReader.Read())
                {
                    if (fieldsDataReader == null)
                    {
                        fieldsDataReader = FillHashSetColumnsDataReader(dataReader);
                    }

                    var item = MapToDynamic(dataReader, fieldsDataReader);

                    result.Add(item);
                }
            }

            return result;
        }

        static HashSet<string> FillHashSetColumnsDataReader(IDataRecord dataRecord)
        {
            var result = new HashSet<string>();

            for (int i = 0; i < dataRecord.FieldCount; i++)
                result.Add(dataRecord.GetName(i));

            return result;
        }

        static dynamic MapToDynamic(IDataReader dataReader, HashSet<string> fieldsDataReader = null)
        {
            if (fieldsDataReader == null)
            {
                fieldsDataReader = FillHashSetColumnsDataReader(dataReader);
            }

            var result = new ExpandoObject() as IDictionary<string, object>;

            foreach (var field in fieldsDataReader)
            {
                result.Add(field, dataReader[field] == DBNull.Value ? null : dataReader[field]);
            }

            return result;
        }
    }
}
