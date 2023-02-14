using HTTPServer.src.Server.Bases;
using HTTPServer.src.Server;
using HTTPServer.src.Response;

namespace HTTPServer.src.Examples.ExampleProject1
{
    public class ExampleServer
    {
        public void Run() 
        {
            IHttpServer httpServer = new HttpListenerServer("127.0.0.1");
            httpServer.SetEndpoints("kek/", (ctx) => new ResponseProvider(ctx).SendTextResponse(@"
                <html>
                    <head>
                    </head>
                    <body>
                        <h1> hythty
                        </h1>
                    </body>
                </html>"));
            httpServer.SetEndpoints("kek2/", (ctx) => new ResponseProvider(ctx).SendHtmlPage());



            httpServer.Listen();
        }
    }
}
