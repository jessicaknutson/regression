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
    public partial class RegistrationNumberForm : Form
    {
        public RegistrationNumberForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            
        }

        public string RegistrationNumber
        {
            get { 
                return textBoxRegNumber.Text; 
            }
            set {
                textBoxRegNumber.Text = value; 
            }
        }

        private void RegistrationNumberForm_Activated(object sender, EventArgs e)
        {
            //base.OnActivated(e);
            //this.TopMost = true;
            //this.TopMost = false;
        }

    }
}
