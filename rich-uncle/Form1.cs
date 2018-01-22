using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace rich_uncle
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Brush b = new SolidBrush(Color.Red);
            int x = 12;
            g.FillEllipse(b, x, 25, 10, 10);
            label10.BackColor = Color.Transparent;
            while (x < 500)
            {
                g.FillEllipse(new SolidBrush(this.BackColor), x, 25, 10, 10);
                x += 2;
                g.FillEllipse(b, x, 25, 10, 10);
                Thread.Sleep(5);
            }
        }
    }
}
