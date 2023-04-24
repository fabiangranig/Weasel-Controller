using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Weasel_Controller
{
    class WeaselManipulatorPanel : Form
    {
        private ComboBox _WeaselDropDown;
        private Weasel[] _weasels;
        private TextBox _txtBox_WeaselPosition;
        private Button _btn_Set;

        public WeaselManipulatorPanel(ref Weasel[] weasels1)
        {
            _weasels = weasels1;

            _txtBox_WeaselPosition = new TextBox();
            _txtBox_WeaselPosition.Location = new Point(10, 55);
            _txtBox_WeaselPosition.Size = new Size(100, 15);
            this.Controls.Add(_txtBox_WeaselPosition);

            _WeaselDropDown = new ComboBox();
            _WeaselDropDown.Location = new Point(10, 10);
            _WeaselDropDown.Size = new Size(100, 15);
            for(int i = 0; i < _weasels.Length; i++)
            {
                _WeaselDropDown.Items.Add(_weasels[i].WeaselName);
            }
            _WeaselDropDown.SelectedIndexChanged += new EventHandler(UpdateTextBoxes);
            this.Controls.Add(_WeaselDropDown);

            _btn_Set = new Button();
            _btn_Set.Location = new Point(10, 80);
            _btn_Set.Size = new Size(100, 30);
            _btn_Set.Text = "Set!";
            _btn_Set.Click += new EventHandler(SetNewInformation);
            this.Controls.Add(_btn_Set);
        }

        private void UpdateTextBoxes(object sender, EventArgs e)
        {
            _txtBox_WeaselPosition.Text = _weasels[_WeaselDropDown.SelectedIndex]._LastPosition.ToString();
        }

        private void SetNewInformation(object sender, EventArgs e)
        {
            _weasels[_WeaselDropDown.SelectedIndex]._LastPosition = Int32.Parse(_txtBox_WeaselPosition.Text);
        }
    }
}
