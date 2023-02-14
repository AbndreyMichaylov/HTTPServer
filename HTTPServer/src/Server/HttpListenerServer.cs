using HTTPServer.src.Server.Bases;
using Serilog;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace HTTPServer.src.Server
{
    public class HttpListenerServer : IHttpServer
    {
        private string _ip;

        private Dictionary<string, Action<HttpListenerContext?>> _endPoints;
        private HttpListener _httpListener;
        public HttpListenerServer(string ip) 
        {
            _ip = $"http://{ip}:8888/";
            _httpListener = new HttpListener();
            _endPoints = new Dictionary<string, Action<HttpListenerContext?>>();
            _httpListener.Start();
            Log.Information($"Server started in {_ip}");
        }
        public void SetEndpoints(string endPoint, Action<HttpListenerContext> handler) 
        {
            _httpListener.Prefixes.Add(_ip + endPoint);
            if (_endPoints.TryAdd(endPoint.Trim('/'), handler))
            {
                Log.Information($"{endPoint} endpoint setting sucesfully");
                return;
            }
            else
            {
                throw new Exception($"Endpoin {endPoint} already exists");
            }
            
        }
        public void Listen()
        {
            while (true)
            {
                ProcessRequest();
            }

        }

        void ProcessRequest()
        {
            var result = _httpListener.BeginGetContext(ListenerCallback, _httpListener);
            result.AsyncWaitHandle.WaitOne();
        }

        void ListenerCallback(IAsyncResult result)
        {
            var context = _httpListener.EndGetContext(result);
            Log.Information($"New connection from {context.Request.Url}");

            var endPoint = context.Request.RawUrl.Trim('/');
            if (_endPoints.TryGetValue(endPoint, out Action<HttpListenerContext> action))
            {
                action?.Invoke(context);
            }
            else //TODO: Create human response for 404 page
            {
                Log.Error($"Nothing handlers to {endPoint} endpoint");
                throw new Exception($"Nothing handlers to {endPoint} endpoint");
            }
        }
    }
}
