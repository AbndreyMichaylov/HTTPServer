using HTTPServer.src.Server;
using HTTPServer.src.Server.Bases;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

using Serilog;
using HTTPServer.src.Response;
using HTTPServer.src.Examples.ExampleProject1;

namespace HTTPServer
{
    public class Program
    {

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                                                  .WriteTo.Console()
                                                  .CreateLogger();
            new ExampleServer().Run();
        }
    }
}