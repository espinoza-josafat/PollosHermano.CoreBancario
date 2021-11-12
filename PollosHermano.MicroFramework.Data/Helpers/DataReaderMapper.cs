using PollosHermano.MicroFramework.Entities.Attributes;
using PollosHermano.MicroFramework.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using TB.ComponentModel;

namespace PollosHermano.MicroFramework.Data.Helpers
{
    public static class DataReaderMapper
    {
        public static IList<T> MapToList<T>(DbDataReader dataReader, Dictionary<string, string> columnsToPropertiesMap = null) where T : new()
        {
            if (dataReader == null)
                throw new ArgumentNullException("dataReader");

            var result = new List<T>();

            if (dataReader.HasRows)
            {
                HashSet<string> fieldsDataReader = null;

                while (dataReader.Read())
                {
                    if (fieldsDataReader == null)
                        fieldsDataReader = FillHashSetColumnsDataReader(dataReader);

                    var item = MapToObject<T>(dataReader, fieldsDataReader, columnsToPropertiesMap);

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

        public static T MapToObject<T>(IDataReader dataReader, Dictionary<string, string> columnsToPropertiesMap = null) where T : new()
        {
            if (dataReader == null)
                throw new ArgumentNullException("dataReader");

            return MapToObject<T>(dataReader, null, columnsToPropertiesMap);
        }

        static T MapToObject<T>(IDataReader dataReader, HashSet<string> fieldsDataReader = null, Dictionary<string, string> columnsToPropertiesMap = null) where T : new()
        {
            if (fieldsDataReader == null)
                fieldsDataReader = FillHashSetColumnsDataReader(dataReader);

            T result = Activator.CreateInstance<T>();

            var properties = result.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(x => x.Name, x => x);

            if (columnsToPropertiesMap != null && columnsToPropertiesMap.Count > 0)
            {
                foreach (var columnToProperty in columnsToPropertiesMap)
                {
                    if (HasColumn(fieldsDataReader, columnToProperty.Key)
                        && properties.TryGetValue(columnToProperty.Value, out PropertyInfo property))
                    {
                        SetValue(result, property, dataReader);
                    }
                }
            }

            foreach (var propertyItem in properties)
            {
                var property = propertyItem.Value;

                if (HasColumn(fieldsDataReader, property.Name))
                    SetValueWithAttributesMapping(result, property, dataReader, fieldsDataReader);
            }

            return result;
        }

        static bool HasColumn(HashSet<string> columnsDataReader, string columnName)
        {
            return columnsDataReader.Contains(columnName);
        }

        static void SetValue(object result, PropertyInfo propertyInfo, IDataReader dataReader)
        {
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                Type type;

                if (propertyInfo.PropertyType == typeof(string))
                    type = typeof(string);
                else
                    type = HelperCommon.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                var @object = dataReader[propertyInfo.Name] == DBNull.Value ? null : dataReader[propertyInfo.Name].To(type);
                propertyInfo.SetValue(result, @object, null);
            }
        }

        static void SetValueWithAttributesMapping(object result, PropertyInfo propertyInfo, IDataReader dataReader, HashSet<string> columnsDataReader)
        {
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                var notMappingAttributes = propertyInfo.GetCustomAttributes<NotMappingAttribute>(true).ToArray();

                if (notMappingAttributes == null || notMappingAttributes.Length == 0)
                {
                    Type type;

                    if (propertyInfo.PropertyType == typeof(string))
                        type = typeof(string);
                    else
                        type = HelperCommon.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                    object @object;

                    var mappingAttributes = propertyInfo.GetCustomAttributes<MappingAttribute>(true).ToArray();
                    if (mappingAttributes.Length > 0)
                    {
                        var columnName = mappingAttributes[0].ColumnName;

                        if (string.IsNullOrWhiteSpace(columnName))
                            return;
                        else
                        {
                            if (HasColumn(columnsDataReader, columnName))
                                @object = dataReader[columnName] == (object)DBNull.Value ? null : Convert.ChangeType(dataReader[columnName], type, null);
                            else
                                return;
                        }
                    }
                    else
                        @object = dataReader[propertyInfo.Name] == DBNull.Value ? null : dataReader[propertyInfo.Name].To(type);

                    propertyInfo.SetValue(result, @object, null);
                }
            }
        }
    }
}
