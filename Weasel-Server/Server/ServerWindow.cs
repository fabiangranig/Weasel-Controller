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
        private List<string> _Hashes;
        private Thread _DestinationsRequestChecker;
        private Weasel[] _Weasels;

        public ServerWindow(ref Weasel[] weasels)
        {
            InitializeComponent();
            _Weasels = weasels;
            _Protocol = new List<string>();
            _Hashes = new List<string>();

            //Add all hashes from the .txt
            string[] hashes_array = System.IO.File.ReadAllLines(@"hashes.txt");
            for(int i = 0; i < hashes_array.Length; i++)
            {
                _Hashes.Add(hashes_array[i]);
            }

            _DestinationsRequestChecker = new Thread(DestinationRequestRuntime);
            _DestinationsRequestChecker.Start();
        }

        public bool IsInHashesList(string hash)
        {
            for(int i = 0; i < _Hashes.Count; i++)
            {
                if(_Hashes[i] == hash)
                {
                    return true;
                }
            }
            return false;
        }

        private void DestinationRequestRuntime()
        {
            while(1 == 1)
            {
                TcpListener Server = new TcpListener(IPAddress.Any, 26000);
                Server.Start();
                TcpClient Client = Server.AcceptTcpClient();
                Stream MessageStream = Client.GetStream();

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
                    for (int i = 0; i < _Weasels.Length; i++)
                    {
                        if(!IsInHashesList(split[3]))
                        {
                            break;
                        }

                        //Get Weasel to send
                        if (_Weasels[i].WeaselName == split[0])
                        {
                            if(split[1] == "Home")
                            {
                                _Weasels[i]._DestinationsWithInformation.Add(new DestinationwithInformation(0, _Weasels[i]._HomePosition, split[2]));
                                break;
                            }
                            if(split[1] == "Kuka1")
                            {
                                _Weasels[i]._DestinationsWithInformation.Add(new DestinationwithInformation(41, "Kuka1", split[2]));
                                break;
                            }

                            //When it is not an defined variable it should be a number
                            _Weasels[i]._DestinationsWithInformation.Add(new DestinationwithInformation(0, Int32.Parse(split[1]), split[2]));
                            break;
                        }
                    }
                }
                catch (IOException)
                {

                }

                Client.Close();
                Server.Stop();

                Thread.Sleep(100);
            }
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
