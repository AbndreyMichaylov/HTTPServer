
namespace HTTPServer.src.Server.Bases
{
    public interface IHttpServer
    {
        Task Listen();
        void SetEndpoints(string endPoint, Action handler);

    }
}
