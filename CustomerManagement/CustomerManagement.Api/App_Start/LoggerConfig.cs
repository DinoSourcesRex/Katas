using Serilog;

namespace CustomerManagement.Api
{
    public class LoggerConfig
    {
        public static void Register()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();
        }
    }
}
