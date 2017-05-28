using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace EmpGrid.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            // Typically we don't need ApplicationInsights in the Diagnostics window.
            TelemetryDebugWriter.IsTracingDisabled = true;

            host.Run();
        }
    }
}
