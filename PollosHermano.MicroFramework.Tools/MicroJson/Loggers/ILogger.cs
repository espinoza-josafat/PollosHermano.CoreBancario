namespace PollosHermano.MicroFramework.Tools.MicroJson.Loggers
{
    public interface ILogger
    {
        void WriteLine(string message, params object[] arguments);
    }
}
