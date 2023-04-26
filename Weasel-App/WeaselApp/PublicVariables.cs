using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WeaselApp
{
    internal class PublicVariables
    {
        public static IPAddress _IP;
        public static string _CurrentUsername;
        public static string _UserHash;

        static PublicVariables()
        {
            _IP = IPAddress.Parse("127.0.0.1");
            _CurrentUsername = "empty";
            _UserHash = "empty";
        }
    }
}
