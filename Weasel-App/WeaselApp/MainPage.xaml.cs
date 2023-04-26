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
        private TcpClient Client;

        public MainPage()
        {
            InitializeComponent();
        }

        public void SendWeaselToPosition(object sender, EventArgs e)
        {
            Client = new TcpClient();
            Client.Connect(PublicVariables._IP, 26000);
            Entry PositionToSendTo = (Entry)FindByName("PositionToSendTo");
            Stream MessageStream = Client.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();
            var input = Convert.ToString(txtBox_WeaselName.Text) + ":" + Convert.ToString(Int32.Parse(PositionToSendTo.Text)) + ":" + PublicVariables._CurrentUsername + ":" + PublicVariables._UserHash;
            if (input != null)
            {
                byte[] buffer = encoder.GetBytes(input);
                MessageStream.Write(buffer, 0, buffer.Length);
                MessageStream.Flush();
            }
        }
    }
}
