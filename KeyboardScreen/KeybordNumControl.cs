using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardScreen
{
    public partial class KeybordNumControl : UserControl
    {
        public KeybordNumControl()
        {
            InitializeComponent();
        }
        //----------------------------------------------------------------------------------------
        private void btn_0_Click(object sender, EventArgs e)
        {
            SendKeys.Send("0");
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("1");
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            SendKeys.Send("2");
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            SendKeys.Send("3");
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            SendKeys.Send("4");
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            SendKeys.Send("5");
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            SendKeys.Send("6");
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            SendKeys.Send("7");
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            SendKeys.Send("8");
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            SendKeys.Send("9");
        }

        private void btn_Dot_Click(object sender, EventArgs e)
        {
            SendKeys.Send(".");
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");

        }
        private void btn_Enter_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ENTER}");
        }
        private void btn_Left_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{LEFT}");
        }
        private void btn_Right_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{RIGHT}");
        }
        private void btn_Down_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{DOWN}");
        }
        private void btn_Up_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{UP}");
        }
    }
}
