using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MineSweeperGUI
{
    public partial class GUIName : Form
    {
        public string PlayerName { get; private set; }

        public GUIName()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            PlayerName = txtName.Text.Trim();

            if (string.IsNullOrEmpty(PlayerName))
            {
                MessageBox.Show("Please enter your name.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}