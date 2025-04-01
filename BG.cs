using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WDED_NAME_GENERATOR
{
    public partial class BG : Form
    {
        public BG()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.Opacity = 0.01;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.Cursor = Cursors.Cross;
        }

        private void BG_Load(object sender, EventArgs e)
        {

        }
    }
}
