using System;
using System.Threading;
using Weasel_Server_2.Logger;
using Weasel_Server_2.CommandHandler.Resolvers;

namespace Weasel_Server_2.ServerHandler
{
    internal class ConsoleQueryWorker
    {
        public static void PickHandler(string command)
        {
            //Send command to Resolver
            switch (command)
            {
                case "help":
                    HelpResolver.ConsoleOutputHelpMenu();
                    LoggerWorker.LogText("Showed help text.");
                    break;

                default:
                    LoggerWorker.LogText("Command '" + command + "' not found.");
                    break;
            }
        }
    }
}
