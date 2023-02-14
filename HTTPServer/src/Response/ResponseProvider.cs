using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            var response = _context.Response;
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            // получаем поток ответа и пишем в него ответ
            response.ContentLength64 = buffer.Length;
            using Stream output = response.OutputStream;
            // отправляем данные
            output.Write(buffer);
            output.Flush();
        }
    }
}
