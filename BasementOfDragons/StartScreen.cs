using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasementOfDragons
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        static public string sName = "";

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtName.Text == " ")
            {
                MessageBox.Show("Please enter a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sName = txtName.Text;

                this.Close();
            }

        }
    }
}
