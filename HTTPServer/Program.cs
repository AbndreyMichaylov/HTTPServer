using HTTPServer.src.Server;
using HTTPServer.src.Server.Bases;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HTTPServer
{
    public class Program
    {

        static void Main(string[] args)
        {
            IHttpServer httpServer = new HttpListenerServer("127.0.0.1");
            httpServer.SetEndpoints("kek/", () => Console.WriteLine("kek"));
            httpServer.SetEndpoints("kek2/", () => Console.WriteLine("kek2"));

            httpServer.Listen();
            #region example
            //httpServer.SetEndpoints("kek/", () => Console.WriteLine("kek"));
            //        HttpListener server = new HttpListener();
            //        // установка адресов прослушки
            //        server.Prefixes.Add("http://127.0.0.1:8888/connection/");
            //        server.Start(); // начинаем прослушивать входящие подключения

            //        // получаем контекст
            //        var context = await server.GetContextAsync();

            //        var response = context.Response;
            //        // отправляемый в ответ код htmlвозвращает
            //        string responseText =
            //            @"<!DOCTYPE html>
            //<html>
            //    <head>
            //        <meta charset='utf8'>
            //        <title>METANIT.COM</title>
            //    </head>
            //    <body>
            //        <h2>Hello METANIT.COM</h2>
            //    </body>
            //</html>";
            //        byte[] buffer = Encoding.UTF8.GetBytes(responseText);
            //        // получаем поток ответа и пишем в него ответ
            //        response.ContentLength64 = buffer.Length;
            //        using Stream output = response.OutputStream;
            //        // отправляем данные
            //        await output.WriteAsync(buffer);
            //        await output.FlushAsync();

            //        Console.WriteLine("Запрос обработан");

            //        server.Stop();
            #endregion
        }
    }
}