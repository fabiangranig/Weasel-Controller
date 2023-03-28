using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Weasel_Controller
{
    class WeaselControlPanel : Form
    {
        private Map _WeaselMap;
        private Weasel[] _Weasels;
        private ComboBox _WeaselDropDown;
        private TextBox _txtBox_Position;
        private Button _btn_SendWeasel;

        public WeaselControlPanel(ref Map map1, ref Weasel[] weasels1)
        {
            //Get weasels with map
            _WeaselMap = map1;
            _Weasels = weasels1;

            //Get all controls into the window
            Label lbl_Weasel = new Label();
            lbl_Weasel.Text = "Weasel: ";
            lbl_Weasel.Location = new Point(10, 10);
            lbl_Weasel.Size = new Size(100, 15);
            this.Controls.Add(lbl_Weasel);
            _WeaselDropDown = new ComboBox();
            _WeaselDropDown.Location = new Point(10, 30);
            _WeaselDropDown.Size = new Size(100, 30);
            for(int i = 0; i < _Weasels.Length; i++)
            {
                _WeaselDropDown.Items.Add(_Weasels[i].WeaselName);
            }
            this.Controls.Add(_WeaselDropDown);
            Label lbl_Position = new Label();
            lbl_Position.Text = "Position: ";
            lbl_Position.Location = new Point(140, 10);
            lbl_Position.Size = new Size(100, 15);
            this.Controls.Add(lbl_Position);
            _txtBox_Position = new TextBox();
            _txtBox_Position.Location = new Point(140, 30);
            _txtBox_Position.Size = new Size(100, 30);
            this.Controls.Add(_txtBox_Position);
            _btn_SendWeasel = new Button();
            _btn_SendWeasel.Location = new Point(10, 60);
            _btn_SendWeasel.Size = new Size(230, 25);
            _btn_SendWeasel.Text = "Send Weasel!";
            _btn_SendWeasel.Click += new EventHandler(SendWeasel);
            this.Controls.Add(_btn_SendWeasel);
        }

        private void SendWeasel(object sender, EventArgs e)
        {
            int selected_weasel = _WeaselDropDown.SelectedIndex;
            int[] path = _WeaselMap.FreePath(_Weasels[selected_weasel]._LastPosition, Int32.Parse(_txtBox_Position.Text));
            _Weasels[selected_weasel].MoveThroughCordinates(path);
        }
    }
}
