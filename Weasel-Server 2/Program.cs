using System;
using Weasel_Server_2.CommandHandler;
using Weasel_Server_2.ServerHandler;
using Weasel_Server_2.Weasel_Server1_Logic;

namespace Weasel_Server_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Starting the server...
            Console.WriteLine("Starting the Weasel-Server 2!");
            Console.WriteLine("Starting the Weasel Controller Foundation.");
            WeaselControllerFoundation WCF = new WeaselControllerFoundation();
            Console.WriteLine("Starting the ConsoleQueryWorker.");
            ConsoleQueryWorker CQW = new ConsoleQueryWorker();
            Console.WriteLine("Open for Post Requests.");
            PostRequestReceiver PostReceiver = new PostRequestReceiver(ref CQW, ref WCF);

            //Get unlimited commands from the user
            string input = String.Empty;
            while (input != "exit")
            {
                //Read in
                Console.Write("> ");
                input = Console.ReadLine();

                //Pass the command to the ConsoleQueryWorker
                CQW.PickHandler(input, ref WCF);
            }
        }
    }
}
