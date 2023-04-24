using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weasel_Controller
{
    internal class ServerWindow : Form
    {
        private ListBox _listbox_Console;
        private List<string> _Protocol;
        private Thread _DestinationsRequestChecker;
        private Weasel[] _Weasels;

        public ServerWindow(ref Weasel[] weasels)
        {
            InitializeComponent();
            _Weasels = weasels;
            _Protocol = new List<string>();
            _DestinationsRequestChecker = new Thread(DestinationRequestRuntime);
            _DestinationsRequestChecker.Start();
        }

        private void DestinationRequestRuntime()
        {
            TcpListener Server = new TcpListener(IPAddress.Any, 26000);
            Server.Start();
            TcpClient Client = Server.AcceptTcpClient();
            Stream MessageStream = Client.GetStream();

            while (true)
            {
                byte[] message = new byte[4096];
                int bytesRead;

                try
                {
                    bytesRead = MessageStream.Read(message, 0, 4096);
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    string encoded_piece = encoder.GetString(message, 0, bytesRead);
                    _Protocol.Add(encoded_piece);

                    //Add destination to corresponding weasel
                    string[] split = encoded_piece.Split(':');
                    for(int i = 0; i < _Weasels.Length; i++)
                    {
                        if (_Weasels[i].WeaselName == split[0])
                        {
                            _Weasels[i]._DestinationsWithSleep.Add(new DestinationwithSleep(Int32.Parse(split[1])));
                        }
                    }
                }
                catch (IOException)
                {
                    break;
                }

                Thread.Sleep(100);
            }

            Client.Close();
            Server.Stop();
        }

        private void InitializeComponent()
        {
            this._listbox_Console = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _listbox_Console
            // 
            this._listbox_Console.FormattingEnabled = true;
            this._listbox_Console.ItemHeight = 20;
            this._listbox_Console.Location = new System.Drawing.Point(12, 12);
            this._listbox_Console.Name = "_listbox_Console";
            this._listbox_Console.Size = new System.Drawing.Size(891, 424);
            this._listbox_Console.TabIndex = 0;
            // 
            // ServerWindow
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(915, 464);
            this.Controls.Add(this._listbox_Console);
            this.Name = "ServerWindow";
            this.Text = "ServerMode";
            this.ResumeLayout(false);

        }
    }
}
