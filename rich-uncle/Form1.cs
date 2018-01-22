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
            Color[] houseColours = new Color[GlobalVariables.NumberOfHouses];
            GlobalVariables glv = new GlobalVariables(this, out houseColours);
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
            paintHouses(houseColours);
        }
        private void paintHouses(Color[] houseColors)
        {
            for (int number = 1; number <= GlobalVariables.NumberOfHouses; number++)
            {
                switch (number)
                {
                    case 1:
                        label1.BackColor = houseColors[number];
                        break;
                    case 2:
                        label2.BackColor = houseColors[number];
                        break;
                    case 3:
                        label3.BackColor = houseColors[number];
                        break;
                    case 4:
                        label4.BackColor = houseColors[number];
                        break;
                    case 5:
                        label5.BackColor = houseColors[number];
                        break;
                    case 6:
                        label6.BackColor = houseColors[number];
                        break;
                    case 7:
                        label7.BackColor = houseColors[number];
                        break;
                    case 8:
                        label8.BackColor = houseColors[number];
                        break;
                    case 9:
                        label9.BackColor = houseColors[number];
                        break;
                    case 10:
                        label10.BackColor = houseColors[number];
                        break;
                    case 11:
                        label11.BackColor = houseColors[number];
                        break;
                    case 12:
                        label12.BackColor = houseColors[number];
                        break;
                    case 13:
                        label13.BackColor = houseColors[number];
                        break;
                    case 14:
                        label14.BackColor = houseColors[number];
                        break;
                    case 15:
                        label15.BackColor = houseColors[number];
                        break;
                    case 16:
                        label16.BackColor = houseColors[number];
                        break;
                    case 17:
                        label17.BackColor = houseColors[number];
                        break;
                    case 18:
                        label18.BackColor = houseColors[number];
                        break;
                    case 19:
                        label19.BackColor = houseColors[number];
                        break;
                    case 20:
                        label20.BackColor = houseColors[number];
                        break;
                    case 21:
                        label21.BackColor = houseColors[number];
                        break;
                    case 22:
                        label22.BackColor = houseColors[number];
                        break;
                    case 23:
                        label23.BackColor = houseColors[number];
                        break;
                    case 24:
                        label24.BackColor = houseColors[number];
                        break;
                    case 25:
                        label25.BackColor = houseColors[number];
                        break;
                    case 26:
                        label26.BackColor = houseColors[number];
                        break;
                    case 27:
                        label27.BackColor = houseColors[number];
                        break;
                    case 28:
                        label28.BackColor = houseColors[number];
                        break;
                    case 29:
                        label29.BackColor = houseColors[number];
                        break;
                    case 30:
                        label30.BackColor = houseColors[number];
                        break;
                    case 31:
                        label31.BackColor = houseColors[number];
                        break;
                    case 32:
                        label32.BackColor = houseColors[number];
                        break;
                    case 33:
                        label33.BackColor = houseColors[number];
                        break;
                    case 34:
                        label34.BackColor = houseColors[number];
                        break;
                    case 35:
                        label35.BackColor = houseColors[number];
                        break;
                    case 36:
                        label36.BackColor = houseColors[number];
                        break;
                    case 37:
                        label37.BackColor = houseColors[number];
                        break;
                    case 38:
                        label38.BackColor = houseColors[number];
                        break;
                    case 39:
                        label39.BackColor = houseColors[number];
                        break;
                    case 40:
                        label40.BackColor = houseColors[number];
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
