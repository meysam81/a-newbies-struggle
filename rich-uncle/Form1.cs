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
            CheckForIllegalCrossThreadCalls = false;
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

        // for every player in the game
        Thread[] t;
        Player[] p;

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!init())
                return;
            

            Color[] houseColours; // going to be initialized in glv constructor, with the 'out' keyword
            GlobalVariables glv = new GlobalVariables(this, out houseColours);

            paintHouses(houseColours); // rondom colorization of 40 houses

            p = new Player[GlobalVariables.NumberOfPlayers];
            t = new Thread[GlobalVariables.NumberOfPlayers];

            // DON't mess with the order, we use this order in the game
            Color[] c = { Color.Blue, Color.Green, Color.Red, Color.Yellow };


            for (int i = 0; i < GlobalVariables.NumberOfPlayers; i++)
            {
                p[i] = new Player(this, c[i]);
                t[i] = new Thread(new ThreadStart(p[i].startPlaying));
                t[i].Name = (i + 1).ToString();
            }
            for (int i = 0; i < GlobalVariables.NumberOfPlayers; i++) 
                t[i].Start(); // players start playing

            Thread turnThrd = new Thread(new ThreadStart(chooseTurn));
            turnThrd.Start(); // start rolling dices
            buttonStart.Enabled = false;
        }

        
        // following are the values initialized in getInfo Form, and we validate by 'gotInfo'
        public bool GotInfo { get; set; }
        private bool init() // get config's from user
        {
            getInfo gf = new getInfo(this);
            gf.ShowDialog();

            if (!GotInfo)
            {
                MessageBox.Show("Please enter correct input!", "Wrong info received",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // main function of this class leis here as it rolls the dice and give turn to players
        private void chooseTurn()
        {
            string[] playersName = { "Blue: ", "Green: ", "Red: ", "Yellow: " };
            int[] playersPoints = new int[GlobalVariables.NumberOfPlayers];


            bool continuePlaying = false; // just for test
            bool firstTime = true;
            do
            {
                writeResultOfPlayers(playersName, playersPoints);
                try
                {
                    if (firstTime)
                    {
                        short maxDice = 0, firstToMove = -1; // to determine the first player
                        for (short i = 0; i < GlobalVariables.NumberOfPlayers; i++)
                        {
                            short tmp = rollTheDice(p[i].MoveColor);
                            if (tmp > maxDice)
                            {
                                maxDice = tmp;
                                firstToMove = i;
                            }
                            Thread.Sleep(1000);
                        }
                        colorizeDiceRoller(p[firstToMove].MoveColor, maxDice);
                        t[firstToMove].Resume();
                    }
                }
                catch (Exception) { } // just in case, if something goes wrong
            } while (continuePlaying);
        }

        // print the game result for user
        private void writeResultOfPlayers(string[] names, int[] points)
        {
            labelPlayers.Text = string.Format(
                "{0}: {1}\n" +
                "{2}: {3}\n",
                names[0], points[0],
                names[1], points[1]);
            if (GlobalVariables.NumberOfPlayers > 2)
            {
                labelPlayers.Text += string.Format(
                "{0}: {1}\n",
                names[2], points[2]);
                if (GlobalVariables.NumberOfPlayers > 3)
                    labelPlayers.Text += string.Format(
                        "{0}: {1}\n",
                        names[3], points[3]);
            }
        }

        // colorize the 40 houses
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


        private void colorizeDiceRoller(Color back, short numberOfDice)
        {
            labelDice.BackColor = back;
            labelDice.Text = numberOfDice.ToString();
        }

        private short rollTheDice(Color back)
        {
            Random generateRandom = new Random(DateTime.Now.Second);
            short result = 0;
            
            for (int i = 0; i < 20; i++)
            {
                result = (short)generateRandom.Next(1, 7);
                colorizeDiceRoller(back, result);
                Thread.Sleep(5);
            }
            return result;
        }
    }
}