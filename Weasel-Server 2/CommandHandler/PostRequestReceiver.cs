using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Weasel_Server_2.ServerHandler;
using Weasel_Server_2.Weasel_Server1_Logic;

namespace Weasel_Server_2.CommandHandler
{
    internal class PostRequestReceiver
    {
        private Thread _Handler;
        private string _URL;
        private ConsoleQueryWorker _CQW;
        private WeaselControllerFoundation _WCF;

        public PostRequestReceiver(ref ConsoleQueryWorker CQW, ref WeaselControllerFoundation WCF)
        {
            _Handler = new Thread(WaitForPostRequest);
            _URL = "http://10.0.1.189:9999/";
            _CQW = CQW;
            _WCF = WCF;

            //Start the Thread
            _Handler.Start();
        }

        private void WaitForPostRequest()
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
                        _CQW.PickHandler(System.Uri.UnescapeDataString(split[1]), ref _WCF);
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
