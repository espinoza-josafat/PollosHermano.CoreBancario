using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PollosHermano.CoreBancario.Common
{
    public static class Configuration
    {
        static readonly Lazy<IConfiguration> _configurationInstance = new Lazy<IConfiguration>(() =>
        {
            var location = Directory.GetCurrentDirectory();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
           .SetBasePath(location)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
            return builder.Build();
        });

        public static IConfiguration Manager { get { return _configurationInstance.Value; } }
    }
}
