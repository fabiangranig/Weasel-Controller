using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Weasel_Controller
{
    class WeaselInformationPanel : Form
    {
        private Weasel[] weasels;
        private Label[,] labels;
        private int InfoSize;
       
        public WeaselInformationPanel(ref Weasel[] weasels1)
        {
            //Get an reference to the weasels
            weasels = weasels1;

            //Get how many infos there are
            InfoSize = 5;

            //Create the labels for an weasel
            labels = new Label[weasels.Length, InfoSize];
            for(int i = 0; i < weasels.Length; i++)
            {
                for(int u = 0; u < InfoSize; u++)
                {
                    labels[i, u] = new Label();

                    if (u == 0)
                    {
                        labels[i, u].Font = new Font(labels[i, u].Font, FontStyle.Bold);
                    }

                    labels[i, u].Size = new Size(100, 50);
                    labels[i, u].Location = new Point(i * 150 + 10, u * 60 + 10);
                    labels[i, u].Text = "Label" + i + u;
                    this.Controls.Add(labels[i, u]);
                }
            }

            //Change the Window Size
            this.Size = new Size(weasels.Length * 150, InfoSize * 60 + 50);

            //Create an updater
            slowUpdateBackend();
            Timer tmr = new Timer();
            tmr.Interval = 1000;
            tmr.Tick += slowUpdate;
            tmr.Start();
        }

        private void slowUpdate(object sender, EventArgs e)
        {
            slowUpdateBackend();
        }

        private void slowUpdateBackend()
        {
            for(int i = 0; i < weasels.Length; i++)
            {
                labels[i, 0].Text = weasels[i].WeaselName;
                labels[i, 1].Text = Convert.ToString(weasels[i].WeaselID);
                labels[i, 2].Text = Convert.ToString(weasels[i]._LastPosition);
                labels[i, 3].Text = Convert.ToString(weasels[i]._BeforeLastPosition);
                labels[i, 4].Text = Convert.ToString(weasels[i].AppOnline);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WeaselPanel
            // 
            this.ClientSize = new System.Drawing.Size(736, 424);
            this.Name = "WeaselPanel";
            this.ResumeLayout(false);
        }
    }
}
