using Serilog;
using System.Net;
using System.Text;

namespace HTTPServer.src.Response
{
    public class ResponseProvider
    {
        HttpListenerContext _context;

        public ResponseProvider(HttpListenerContext context) 
        {
            _context = context;
        }

        public void SendTextResponse(string text)         
        {
            try 
            {
                var response = _context.Response;
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                response.ContentLength64 = buffer.Length;
                using Stream output = response.OutputStream;
                output.Write(buffer);
                output.Flush();
                Log.Information($"Sended response to {_context.Request.Url}");
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
            }

        }

        public void SendHtmlPage() 
        {
            try
            {
                var response = _context.Response;
                byte[] buffer = Encoding.UTF8.GetBytes(File.ReadAllText(@"C:\\Users\\79775\\source\\repos\\HTTPServer\\HTTPServer\\src\\Examples\\ExampleProject1\\Pages\\Index.html"));
                response.ContentLength64 = buffer.Length;
                using Stream output = response.OutputStream;
                output.Write(buffer);
                output.Flush();
                Log.Information($"Sended response to {_context.Request.Url}");
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }
    }
}
