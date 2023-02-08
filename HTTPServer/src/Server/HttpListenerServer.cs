using HTTPServer.src.Server.Bases;
using System.Net;
using System.Net.Sockets;
using Timer = System.Timers.Timer;

namespace HTTPServer.src.Server
{
    public class HttpListenerServer : IHttpServer
    {
        static readonly object _lock = new object();
        private string _ip;
        private int _requestAwaitTime = 300;

        private Dictionary<string, Action> _endPoints;
        private Timer _requestTimer;
        private HttpListener _httpListener;
        public HttpListenerServer(string ip) 
        {
            _ip = $"http://{ip}:8888/";
            _httpListener = new HttpListener();
            _endPoints = new Dictionary<string, Action>();
            _requestTimer = new Timer();
            _requestTimer.Interval = _requestAwaitTime;
            _httpListener.Start();
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
        public void Listen()
        {
            while (true)
            {
                GetContext();
            }
        }

        private void ProcessRequest(HttpListener httpListener) 
        {
            _requestTimer.Start();
            var context = httpListener.GetContext();
            Console.WriteLine($"{context.GetHashCode()}");
            Console.WriteLine($"{_httpListener.GetHashCode()}");
            var endPoint = context.Request.RawUrl.Trim('/');
            Console.WriteLine(endPoint);
            if (_endPoints.TryGetValue(endPoint, out Action action))
            {

                action.Invoke();
            }
            else
            {
                Console.WriteLine("Nothing end");
                throw new Exception($"Nothing handlers to {endPoint} endpoint");
            }
        }

        private void GetContext(HttpListener httpListener, out HttpListenerContext listenerContext) 
        {
            var context = httpListener.GetContext();

            listenerContext = context;
        }
    }
}
