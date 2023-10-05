using System;
using System.Threading;
using Weasel_Server_2.Logger;
using Weasel_Server_2.CommandHandler.Resolvers;
using Weasel_Server_2.Weasel_Server1_Logic;

namespace Weasel_Server_2.ServerHandler
{
    internal class ConsoleQueryWorker
    {
        public void PickHandler(string command, ref WeaselControllerFoundation WCF)
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

                case "weasel":
                    WeaselResolver.InputWorker(split_string, ref WCF);
                    break;

                default:
                    LoggerWorker.LogText("Command '" + command + "' not found.");
                    break;
            }
        }

        private string[] GetSpaceSplitString(string command)
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
