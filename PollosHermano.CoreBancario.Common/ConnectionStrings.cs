using Microsoft.Extensions.Configuration;
using System;

namespace PollosHermano.CoreBancario.Common
{
    public static class ConnectionStrings
    {
        static readonly Lazy<string> _PollosHermanoCoreBancarioDBConnectionStringInstance = new Lazy<string>(() =>
        {
            return Configuration.Manager.GetConnectionString("PollosHermanoCoreBancarioDBConnectionString");
        });

        static readonly Lazy<string> SysCoreConnectionStringInstance = new Lazy<string>(() =>
        {
            return Configuration.Manager.GetConnectionString("SysCoreConnectionString");
        });

        public static string PollosHermanoCoreBancarioDBConnectionString { get { return _PollosHermanoCoreBancarioDBConnectionStringInstance.Value; } }

        public static string SysCoreConnectionString { get { return SysCoreConnectionStringInstance.Value; } }
    }
}
