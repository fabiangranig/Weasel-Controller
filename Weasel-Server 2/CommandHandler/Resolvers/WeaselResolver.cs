using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weasel_Server_2.Weasel_Server1_Logic;

namespace Weasel_Server_2.CommandHandler.Resolvers
{
    internal class WeaselResolver
    {
        public static void InputWorker(string[] CommandWithArguments, ref WeaselControllerFoundation WCF)
        {
            if (CommandWithArguments[1] == "move")
            {
                WCF.MoveWeasel(Int32.Parse(CommandWithArguments[2]), CommandWithArguments[3]);
            }
        }
    }
}
