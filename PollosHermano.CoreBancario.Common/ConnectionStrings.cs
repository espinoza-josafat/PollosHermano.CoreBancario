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

        static readonly Lazy<string> IdentityConnectionStringInstance = new Lazy<string>(() =>
        {
            return Configuration.Manager.GetConnectionString("IdentityConnectionString");
        });

        public static string PollosHermanoCoreBancarioDBConnectionString { get { return _PollosHermanoCoreBancarioDBConnectionStringInstance.Value; } }

        public static string IdentityConnectionString { get { return IdentityConnectionStringInstance.Value; } }
    }
}
