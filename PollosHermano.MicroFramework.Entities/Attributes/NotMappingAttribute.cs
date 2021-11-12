using System;

namespace PollosHermano.MicroFramework.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    [Serializable]
    public class NotMappingAttribute : Attribute
    {
    }
}
