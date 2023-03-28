using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT1000.GUI
{
    public partial class ProcessInformationForm : Form
    {
        public ProcessInformationForm()
        {
            InitializeComponent();
        }

        public string GetProcessInformation()
        {
            return tBoxInfo.Text;
        }

        public void Clear()
        {
            tBoxInfo.Text = string.Empty;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProcessInformationForm_Shown(object sender, EventArgs e)
        {
            tBoxInfo.Text = "";
            tBoxInfo.Focus();
        }
    }
}
