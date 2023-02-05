using HTTPServer.src.Server.Bases;
using System.Net;

namespace HTTPServer.src.Server
{
    public class HttpListenerServer : IHttpServer
    {
        private Dictionary<string, Action> _endPoints;
        private string _ip;
        private HttpListener _httpListener;
        public HttpListenerServer(string ip) 
        {
            _ip = $"http://{ip}:8888/";
            _httpListener = new HttpListener();
            _endPoints = new Dictionary<string, Action>();
        }
        public void SetEndpoints(string endPoint, Action handler) 
        {
            _httpListener.Prefixes.Add(_ip + endPoint);
            Console.WriteLine(_ip + endPoint);
            if (_endPoints.TryAdd(endPoint.Trim('/'), handler)) 
            {
                return;
            }
            else throw new Exception("Endpoin already exists");
            
        }
        public async Task Listen()
        {
            while (true)
            { 
                var context = await _httpListener.GetContextAsync();
                var endPoint = context.Request.RawUrl.Trim('/');
                Console.WriteLine(endPoint);
                if (_endPoints.TryGetValue(endPoint, out Action action))
                {

                    action.Invoke();
                    _httpListener.Stop();
                }
                else
                {
                    Console.WriteLine("Nothing end");
                    throw new Exception($"Nothing handlers to {endPoint} endpoint");
                }
            }
        }

        ~HttpListenerServer() { _httpListener.Close(); }
    }
}
