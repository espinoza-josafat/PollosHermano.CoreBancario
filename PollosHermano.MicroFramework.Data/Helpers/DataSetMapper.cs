using PollosHermano.MicroFramework.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using TB.ComponentModel;

namespace PollosHermano.MicroFramework.Data.Helpers
{
    public static class DataSetMapper
    {
        public static IList<T> MapDataTableToList<T>(DataTable dataTable, Dictionary<string, string> columnsToPropertiesMap = null) where T : class, new()
        {
            if (dataTable == null)
                throw new ArgumentNullException("dataTable");

            var result = new List<T>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var item = MapDataRowToObject<T>(dataTable.Rows[i], columnsToPropertiesMap);

                result.Add(item);
            }

            return result;
        }

        public static T MapDataRowToObject<T>(DataRow dataRow, Dictionary<string, string> columnsToPropertiesMap = null) where T : class, new()
        {
            if (dataRow == null)
                throw new ArgumentNullException("dataRow");

            T result = Activator.CreateInstance<T>();

            var properties = result.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(x => x.Name, x => x);

            if (columnsToPropertiesMap != null && columnsToPropertiesMap.Count > 0)
            {
                foreach (var columnToProperty in columnsToPropertiesMap)
                {
                    var columnName = columnToProperty.Key;

                    if (dataRow.Table.Columns.Contains(columnName))
                    {
                        var propertyName = columnToProperty.Value;


                        if (properties.TryGetValue(propertyName, out PropertyInfo property))
                            SetValue(result, property, dataRow, columnName);
                    }
                }
            }

            for (int i = 0; i < dataRow.Table.Columns.Count; i++)
            {
                var propertyName = dataRow.Table.Columns[i].ColumnName;

                if (properties.TryGetValue(propertyName, out PropertyInfo property))
                    SetValue(result, property, dataRow, propertyName);
            }

            return result;
        }

        static void SetValue(object result, PropertyInfo propertyInfo, DataRow dataRow, string columnName)
        {
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                Type type;

                if (propertyInfo.PropertyType == typeof(string))
                    type = typeof(string);
                else
                    type = HelperCommon.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                var @object = dataRow[columnName] == DBNull.Value ? null : dataRow[columnName].To(type);
                propertyInfo.SetValue(result, @object, null);
            }
        }
    }
}
