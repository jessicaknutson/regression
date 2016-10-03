using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNsure_Regression_1
{
    public partial class SSNNumberForm : Form
    {
        public SSNNumberForm()
        {
            InitializeComponent();
        }

        public string SSNNumber
        {
            get
            {
                return textBoxExistingSSN.Text;
            }
            set
            {
                textBoxExistingSSN.Text = value;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

        private void SSNNumberForm_Activated(object sender, EventArgs e)
        {

        }
    }
}
