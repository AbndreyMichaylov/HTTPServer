
using System.Net;

namespace HTTPServer.src.Server.Bases
{
    public interface IHttpServer
    {
        void Listen();
        void SetEndpoints(string endPoint, Action<HttpListenerContext> handler);

    }
}
