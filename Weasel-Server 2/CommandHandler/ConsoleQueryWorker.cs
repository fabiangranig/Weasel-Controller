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
            //Break command into parts
            string[] split_string = GetSpaceSplitString(command);
            
            //Send command to Resolver
            switch (split_string[0])
            {
                case "help":
                    HelpResolver.ConsoleOutputHelpMenu();
                    LoggerWorker.LogText("Showed help text.");
                    break;

                case "move":

                    break;

                default:
                    LoggerWorker.LogText("Command '" + command + "' not found.");
                    break;
            }
        }

        private static string[] GetSpaceSplitString(string command)
        {
            string[] split_string;
            if (command.Contains(" "))
            {
                return command.Split(' ');
            }
            else
            {
                split_string = new string[1];
                split_string[0] = command;
                return split_string;
            }
        }
    }
}
