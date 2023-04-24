using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeaselApp
{
    public partial class MainPage : ContentPage
    {
        private IPAddress IP;
        private TcpClient Client;

        public MainPage()
        {
            InitializeComponent();
        }

        public void SetIP(object sender, EventArgs e)
        {
            Entry IP_TextBox = (Entry)FindByName("IP_TextBox");
            IP = IPAddress.Parse(Convert.ToString(IP_TextBox.Text));
            Client = new TcpClient();
            Client.Connect(IP, 26000);
        }

        public void SendWeaselToPosition(object sender, EventArgs e)
        {
            Entry PositionToSendTo = (Entry)FindByName("PositionToSendTo");
            string userInput = PositionToSendTo.Text;
            EnteredPosition.Text = userInput;
            SendWeaselOverTCP(Int32.Parse(userInput));
        }

        public void SendWeaselOverTCP(int position)
        {
            Stream MessageStream = Client.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();
            var input = "MC6" + ":" + Convert.ToString(position);
            if (input != null)
            {
                byte[] buffer = encoder.GetBytes(input);
                MessageStream.Write(buffer, 0, buffer.Length);
                MessageStream.Flush();
            }
        }
    }
}
