using System.Diagnostics;

namespace Operators.Moddleware {

    public class Program {

        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions() {
                Args = args,
                ContentRootPath = AppContext.BaseDirectory,
                ApplicationName = Process.GetCurrentProcess().ProcessName
            });

            //enable logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var startup = new Startup(builder.Configuration);

            // calling ConfigureServices method
            startup.ConfigureServices(builder.Services);

            //run API as windows service
            builder.Host.UseWindowsService(options => {
                options.ServiceName = "OperatorMiddleware";
            });

            // calling Configure method
            var app = builder.Build();
            startup.Configure(app);
        }
    }

}
