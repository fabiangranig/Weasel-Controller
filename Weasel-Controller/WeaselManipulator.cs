using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Weasel_Controller
{
    class WeaselManipulator : Form
    {
        private ComboBox _WeaselDropDown;
        private Weasel[] _weasels;
        private TextBox _WeaselPosition;
        private Button _btn_Set;

        public WeaselManipulator(ref Weasel[] weasels1)
        {
            _weasels = weasels1;

            _WeaselDropDown = new ComboBox();
            _WeaselDropDown.Location = new Point(10, 10);
            _WeaselDropDown.Size = new Size(100, 15);
            this.Controls.Add(_WeaselDropDown);
        }
    }
}
