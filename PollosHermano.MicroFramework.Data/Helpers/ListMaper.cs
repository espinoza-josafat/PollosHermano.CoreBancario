using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PollosHermano.MicroFramework.Data.Helpers
{
    public static class ListMaper
    {
        /// <summary>
        /// Transforma Una Lista Generica En Un DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(IList<T> model)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor pd in properties)
                table.Columns.Add(pd.Name, Nullable.GetUnderlyingType(pd.PropertyType) ?? pd.PropertyType);
            foreach (T item in model)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
