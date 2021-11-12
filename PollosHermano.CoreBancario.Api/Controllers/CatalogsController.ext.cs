using PollosHermano.MicroFramework.Tools;
using PollosHermano.MicroFramework.Tools.Exceptions;
using PollosHermano.MicroFramework.Tools.Http;
using PollosHermano.CoreBancario.Application.Catalogs;
using PollosHermano.CoreBancario.Application.Catalogs.Contracts;
using PollosHermano.CoreBancario.Data.Catalogs.Engines;
using PollosHermano.CoreBancario.Data.Catalogs.Engines.Contracts;
using PollosHermano.CoreBancario.Entities.Catalogs.Enums;
using PollosHermano.CoreBancario.Entities.Catalogs.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PollosHermano.CoreBancario.Api.Controllers
{
    public partial class CatalogsController
    {
        async Task<object> ExecuteAsync(RunExecuteModel model, RunCatalogModel catalog)
        {
            var innerModel = (ExecuteModel)null;

            if (model != null)
                innerModel = new ExecuteModel
                {
                    Parameters = model.Parameters,
                    Request = model.Request
                };

            return catalog.Type switch
            {
                CatalogType.Static => catalog.Configuration,
                CatalogType.DataBase => await CatalogTypeDataBase(((JObject)catalog.Configuration).ToObject<DataBaseConfiguration>(), innerModel),
                CatalogType.Api => await CatalogTypeApi(((JObject)catalog.Configuration).ToObject<ApiConfiguration>(), innerModel),
                CatalogType.Plugin => await CatalogTypePlugin(((JObject)catalog.Configuration).ToObject<PlugInConfiguration>(), innerModel),
                _ => null,
            };
        }

        async Task<object> CatalogTypeDataBase(DataBaseConfiguration configuration, ExecuteModel model)
        {
            if (configuration == null)
                throw new DataValidationException("La configuración de base de datos no puede ser nula");

            var dataBaseEngineConfiguration = new DataBaseEngineConfiguration();

            if (string.IsNullOrWhiteSpace(configuration.Query))
                throw new DataValidationException("El campo 'Query' no puede ser nulo o vacío");

            dataBaseEngineConfiguration.Query = configuration.Query;

            if (string.IsNullOrWhiteSpace(configuration.ConnectionString) && string.IsNullOrWhiteSpace(configuration.ConnectionStringName))
                throw new DataValidationException("No se ha configurado una cadena de conexión");

            if (!string.IsNullOrWhiteSpace(configuration.ConnectionString))
                dataBaseEngineConfiguration.ConnectionString = configuration.ConnectionString;
            else if (!string.IsNullOrWhiteSpace(configuration.ConnectionStringName))
            {
                try
                {
                    var connectionString = Environment.GetEnvironmentVariable(configuration.ConnectionStringName);
                    if (string.IsNullOrWhiteSpace(connectionString))
                        throw new DataValidationException($"No se ha configurado la cadena de conexión: '{configuration.ConnectionStringName}'");
                    dataBaseEngineConfiguration.ConnectionString = connectionString;
                }
                catch (Exception)
                {
                    throw new DataValidationException($"No se ha configurado la cadena de conexión: '{configuration.ConnectionStringName}'");
                }
            }

            dataBaseEngineConfiguration.Type = configuration.Type;
            dataBaseEngineConfiguration.Parameters = configuration.Parameters;

            IDataBaseEngine engine = null;

            switch (configuration.DatabaseEngine)
            {
                case DatabaseEngine.SqlServer:
                    engine = new SqlServerEngine();
                    break;
            }

            if (model?.Parameters?.Count > 0)
            {
                dataBaseEngineConfiguration.Query = HelperBindings.BindingParametersCatalog(dataBaseEngineConfiguration.Query, model.Parameters);

                if (dataBaseEngineConfiguration.Parameters?.Count > 0)
                    foreach (var parameter in dataBaseEngineConfiguration.Parameters)
                    {
                        parameter.Key = HelperBindings.BindingParametersCatalog(parameter.Key, model.Parameters);
                        parameter.Value = HelperBindings.BindingParametersCatalog(parameter.Value, model.Parameters);
                    }
            }

            return await engine?.ExecuteAsync(dataBaseEngineConfiguration);
        }

        static async Task<object> CatalogTypeApi(ApiConfiguration configuration, ExecuteModel model)
        {
            if (configuration == null)
                throw new DataValidationException("La configuración de la petición no puede ser nula");

            if (string.IsNullOrWhiteSpace(configuration.Endpoint))
                throw new DataValidationException("El campo 'Endpoint' no puede ser nulo o vacío");

            string request = null;

            if (model?.Request != null)
            {
                request = JsonConvert.SerializeObject(model.Request);
            }
            else if (!string.IsNullOrWhiteSpace(configuration.Request))
            {
                request = configuration.Request;
            }

            if (model?.Parameters?.Count > 0)
            {
                configuration.Endpoint = HelperBindings.BindingParametersCatalog(configuration.Endpoint, model.Parameters);

                if (configuration.Headers?.Count > 0)
                    foreach (var header in configuration.Headers)
                    {
                        configuration.Headers[header.Key] = HelperBindings.BindingParametersCatalog(header.Value, model.Parameters);
                    }

                if (!string.IsNullOrWhiteSpace(request))
                {
                    request = HelperBindings.BindingParametersCatalog(request, model.Parameters);
                }
            }

            string result = null;

            switch (configuration.Type)
            {
                case TypeApi.Get:
                    result = await RequestHttp.GetWithErrorAsync<string>(configuration.Endpoint, configuration.Headers);
                    break;
                case TypeApi.Post:
                    result = await RequestHttp.PostJsonWithErrorAsync<string>(configuration.Endpoint, request, configuration.Headers);
                    break;
                case TypeApi.Put:
                    result = await RequestHttp.PutJsonWithErrorAsync<string>(configuration.Endpoint, request, configuration.Headers);
                    break;
                case TypeApi.Patch:
                    result = await RequestHttp.PatchJsonWithErrorAsync<string>(configuration.Endpoint, request, configuration.Headers);
                    break;
                case TypeApi.Delete:
                    result = await RequestHttp.DeleteJsonWithErrorAsync<string>(configuration.Endpoint, request, configuration.Headers);
                    break;
            }

            return JToken.Parse(result);
        }

        async Task<object> CatalogTypePlugin(PlugInConfiguration configuration, ExecuteModel model)
        {
            if (configuration == null)
                throw new DataValidationException("La configuración de la petición no puede ser nula");

            if (string.IsNullOrWhiteSpace(configuration.AssemblyPath))
                throw new DataValidationException("El campo 'AssemblyPath' no puede ser nulo o vacío");

            configuration.AssemblyPath = configuration.AssemblyPath.Trim();

            if (configuration.IsAssemblyPathRelative)
            {
                configuration.AssemblyPath = Path.Combine(_webHostEnvironment.ContentRootPath, configuration.AssemblyPath);
            }

            if (string.IsNullOrWhiteSpace(configuration.AssemblyName))
                throw new DataValidationException("El campo 'AssemblyName' no puede ser nulo o vacío");

            configuration.AssemblyName = configuration.AssemblyName.Trim();

            if (string.IsNullOrWhiteSpace(configuration.FullNameClass))
                throw new DataValidationException("El campo 'FullNameClass' no puede ser nulo o vacío");

            configuration.FullNameClass = configuration.FullNameClass.Trim();

            if (!Directory.Exists(configuration.AssemblyPath))
                throw new DataValidationException("El directorio especificado en el campo 'AssemblyPath' no existe");

            if (!configuration.AssemblyName.ToLower().EndsWith(".dll"))
                configuration.AssemblyName += ".dll";

            var fullAssemblyPath = Path.Combine(configuration.AssemblyPath, configuration.AssemblyName);

            if (!System.IO.File.Exists(fullAssemblyPath))
                throw new DataValidationException("El ensamblado no existe en la ruta especificada");

            var assembly = LoadPlugin(configuration.AssemblyPath, fullAssemblyPath);
            if (assembly == null)
                throw new DataValidationException("No se pudo cargar el ensamblado al contexto");

            var command = CreateCommand(assembly, configuration.FullNameClass);

            object parameter = null;

            if (model?.Request != null && configuration.CastModel != null && !string.IsNullOrWhiteSpace(configuration.CastModel.AssemblyName) && !string.IsNullOrWhiteSpace(configuration.CastModel.FullNameClass))
            {
                configuration.CastModel.FullNameClass = configuration.CastModel.FullNameClass.Trim();

                var myType = model.Request.GetType().FullName;

                if (model.Request.GetType().FullName == configuration.CastModel.FullNameClass)
                {
                    parameter = model.Request;
                }
                else
                {
                    configuration.CastModel.AssemblyName = configuration.CastModel.AssemblyName.Trim();

                    var assemblyContext = _loadContext.Assemblies.FirstOrDefault(x => x.GetName().Name == configuration.CastModel.AssemblyName);

                    Type type = null;

                    if (assemblyContext == null)
                    {
                        var typeName = $"{configuration.CastModel.FullNameClass}, {configuration.CastModel.AssemblyName}";

                        type = Type.GetType(typeName, false, false);
                    }
                    else
                    {
                        var typeName = configuration.CastModel.FullNameClass;

                        type = assemblyContext.GetType(typeName, false, false);
                    }

                    if (type == null)
                        throw new DataValidationException("No se pudo obtener el tipo a deserializar");

                    var json = model.Request.ToString();
                    parameter = JsonConvert.DeserializeObject(json, type);
                }
            }

            return await command.ExecuteAsync(parameter);
        }

        Assembly LoadPlugin(string pluginLocation, string assemblyPath)
        {
            _loadContext = new PluginLoadContext(pluginLocation);
            return _loadContext.LoadFromAssemblyPath(assemblyPath);
        }

        static ICommandBase CreateCommand(Assembly assembly, string fullNameClass)
        {
            var type = assembly.GetType(fullNameClass, false, false);
            if (type == null)
            {
                throw new DataValidationException($"No se encontró el nombre de la clase: {fullNameClass} en el ensamblado: {assembly.FullName}");
            }

            if (!typeof(ICommandBase).IsAssignableFrom(type))
            {
                throw new DataValidationException($"La clase: {fullNameClass} no implementa la interface: {typeof(ICommandBase).Name}");
            }

            var result = Activator.CreateInstance(type) as ICommandBase;
            if (result == null)
            {
                throw new DataValidationException($"No se pudo instanciar la clase: {fullNameClass}");
            }

            return result;
        }
    }
}
