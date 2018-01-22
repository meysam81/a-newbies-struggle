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
        public Point getHouseLocation(short number) // to get the location of the houses
        {
            switch (number)
            {
                case 1:
                    return label1.Location;
                case 2:
                    return label2.Location;
                case 3:
                    return label3.Location;
                case 4:
                    return label4.Location;
                case 5:
                    return label5.Location;
                case 6:
                    return label6.Location;
                case 7:
                    return label7.Location;
                case 8:
                    return label8.Location;
                case 9:
                    return label9.Location;
                case 10:
                    return label10.Location;
                case 11:
                    return label11.Location;
                case 12:
                    return label12.Location;
                case 13:
                    return label13.Location;
                case 14:
                    return label14.Location;
                case 15:
                    return label15.Location;
                case 16:
                    return label16.Location;
                case 17:
                    return label17.Location;
                case 18:
                    return label18.Location;
                case 19:
                    return label19.Location;
                case 20:
                    return label20.Location;
                case 21:
                    return label21.Location;
                case 22:
                    return label22.Location;
                case 23:
                    return label23.Location;
                case 24:
                    return label24.Location;
                case 25:
                    return label25.Location;
                case 26:
                    return label26.Location;
                case 27:
                    return label27.Location;
                case 28:
                    return label28.Location;
                case 29:
                    return label29.Location;
                case 30:
                    return label30.Location;
                case 31:
                    return label31.Location;
                case 32:
                    return label32.Location;
                case 33:
                    return label33.Location;
                case 34:
                    return label34.Location;
                case 35:
                    return label35.Location;
                case 36:
                    return label36.Location;
                case 37:
                    return label37.Location;
                case 38:
                    return label38.Location;
                case 39:
                    return label39.Location;
                case 40:
                    return label40.Location;
                default:
                    return new Point(0, 0);
            }
        }

        


        private void buttonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            GlobalVariables glv = new GlobalVariables(this);
            Player[] p = new Player[4];
            Thread[] t = new Thread[4];
            Color[] c = { Color.Red, Color.Blue, Color.Green, Color.Yellow };
            for (int i = 0; i < 4; i++)
            {
                p[i] = new Player(this, c[i]);
                t[i] = new Thread(new ThreadStart(p[i].initialPoisitioning));
                t[i].Name = (i + 1).ToString();
            }
            for (int i = 0; i < 4; i++)
                t[i].Start();
        }
    }
}
