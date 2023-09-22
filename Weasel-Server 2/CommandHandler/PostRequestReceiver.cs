using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Weasel_Server_2.ServerHandler;

namespace Weasel_Server_2.CommandHandler
{
    internal class PostRequestReceiver
    {
        static Thread _Handler;
        static string _URL;

        public PostRequestReceiver()
        {
            _Handler = new Thread(WaitForPostRequest);
            _URL = "http://192.232.0.112:9999/";

            //Start the Thread
            _Handler.Start();
        }

        private static void WaitForPostRequest()
        {
            //Listener erstellen und starten
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(_URL);
            listener.Start();

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                if (request.HttpMethod == "POST")
                {
                    string requestBody;
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        requestBody = reader.ReadToEnd();
                    }

                    //Handle it to the ConsoleQueryWorker
                    if(requestBody.Contains("data"))
                    {
                        string[] split = requestBody.Split('=');
                        ConsoleQueryWorker.PickHandler(split[1]);
                    }

                    byte[] responseBytes = Encoding.UTF8.GetBytes("POST request received successfully.");
                    context.Response.ContentLength64 = responseBytes.Length;
                    context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                }

                context.Response.Close();
            }
        }
    }
}
