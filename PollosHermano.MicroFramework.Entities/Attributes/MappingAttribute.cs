using System;

namespace PollosHermano.MicroFramework.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    [Serializable]
    public class MappingAttribute : Attribute
    {
        public string ColumnName { get; set; }

        public MappingAttribute(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentNullException(nameof(columnName));

            ColumnName = columnName;
        }
    }
}
