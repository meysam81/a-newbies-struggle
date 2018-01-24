using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static rich_uncle.GlobalVariables;
namespace rich_uncle
{
    public partial class FormMain : Form
    {
        // ==================================== class private fields ========================================

        // for every player in the game
        Thread[] t; // players thread
        Player[] p;
        Thread turnThrd; // dice roll thread
        bool buy;
        System.Media.SoundPlayer diceSound = new System.Media.SoundPlayer("dice.wav");

        // ================================== class public functions ========================================
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
        public bool GotInfo { get; set; }

        // ================================== class private functions =======================================
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!init())
                return;


            GlobalVariables glv = new GlobalVariables(this);

            paintHouses(); // rondom colorization of 40 houses
            showNamesAndPrices();

            p = new Player[NumberOfPlayers];
            t = new Thread[NumberOfPlayers];

            // DON't mess with the order, we use this order in the game
            Color[] c = { Color.DodgerBlue, Color.Green, Color.Red, Color.Yellow };

            turnThrd = new Thread(new ThreadStart(chooseTurn));

            for (short i = 0; i < NumberOfPlayers; i++)
            {
                p[i] = new Player(this, c[i], i, turnThrd, 12, 12);
                t[i] = new Thread(new ThreadStart(p[i].startPlaying));
                t[i].Name = i.ToString();
            }
            for (int i = 0; i < NumberOfPlayers; i++)
                t[i].Start(); // players start playing

            turnThrd.Start(); // start rolling dices

            buttonStart.Enabled = false;
            buttonStart.Visible = false;

        }
        // following are the values initialized in getInfo Form, and we validate by 'gotInfo'
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
            string[] playersName = { "Blue", "Green", "Red", "Yellow" };

            short[] turns = { -1, -1, -1, -1 };

            // first dice roll
            writeResultOfPlayers(playersName);
            showBankDeposit(BankDeposit);

            short countTurns = 0; // iterate to the number of players in turns
            short nextPosition = 0; // house to be occupied
            try // first roll of the dice
            {
                short maxDice = 0, firstToMove = -1; // to determine the first player
                short currentDice = 0;
                for (short i = 0; i < NumberOfPlayers; i++)
                {


                    currentDice = rollTheDice(p[i].MoveColor);
                    if (currentDice > maxDice)
                    {
                        maxDice = currentDice;
                        firstToMove = i;
                    }
                    p[i].NumberOfMovements = currentDice; // everyone's turn is determined here
                }

                turns[countTurns++] = firstToMove;

                colorizeDiceRoller(p[firstToMove].MoveColor, maxDice);

                nextPosition = (short)(p[turns[0]].CurrentHouse + p[turns[0]].NumberOfMovements);

                t[firstToMove].Resume();
                Thread.CurrentThread.Suspend();

                for (short i = 0; i < NumberOfPlayers; i++)
                    if (i != firstToMove)
                        turns[countTurns++] = i;


                buyCurrentHouse(turns[0], playersName[turns[0]],
                    nextPosition); // should we buy?


                if (currentDice == 6)
                    countTurns = 0;
                else
                    countTurns = 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } // just in case, if something goes wrong



            bool gameFinished = true;

            do
            {

                gameFinished = true;


                if (countTurns >= NumberOfPlayers)
                    countTurns = 0;

                writeResultOfPlayers(playersName);
                showBankDeposit(BankDeposit);


                try
                {

                    // check for game end
                    for (ushort i = 1; i <= NumberOfHouses; i++)
                        if (HouseOwner[i] < 0) // game still continues; houses are left to buy
                            gameFinished = false;




                    short currentTurn = turns[countTurns];

                    short currentDice = rollTheDice(p[currentTurn].MoveColor);
                    if (currentDice == 6)
                        countTurns--; // a player with dice 6, gets a reward
                    p[currentTurn].NumberOfMovements = currentDice;

                    colorizeDiceRoller(p[currentTurn].MoveColor, p[currentTurn].NumberOfMovements);


                    nextPosition = (short)(p[currentTurn].CurrentHouse +
                        p[currentTurn].NumberOfMovements);

                    if (nextPosition > 40) // player finished a round
                    {
                        nextPosition %= 40;

                        changeGroupBuyButtons(false, BackColor, Color.LightGray,
                            string.Format("Player {0} passed one round and got {1} from bank!",
                            playersName[currentTurn], FinishRoundBonus));


                        p[currentTurn].PlayerDeposit += (short)FinishRoundBonus;
                        BankDeposit -= FinishRoundBonus;
                    }


                    t[currentTurn].Resume(); // let the player move for it's turn
                    // wait for the player to finish moving, then roll the dice again
                    Thread.CurrentThread.Suspend();



                    if (HouseOwner[nextPosition] == -1) // the house is not bought
                        buyCurrentHouse(currentTurn, playersName[currentTurn], nextPosition);
                    else if (currentTurn != HouseOwner[nextPosition]) // it is bought, not by itself definitely
                        rentHouse(currentTurn, playersName[currentTurn], nextPosition,
                            playersName[HouseOwner[nextPosition]], HouseOwner[nextPosition]);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, ex.Message + ex.Source,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                } // just in case, if something goes wrong

                

                countTurns++;
            } while (!gameFinished); // condition is for test ONLY




        }
        private void buyCurrentHouse(short playerIndex, string playerName, short houseToBeBougth)
        {

            ushort cost = BuyHouse[houseToBeBougth];

            changeGroupBuyButtons(true, Color.Goldenrod, Color.LightGray,
                string.Format("Player {0} buy house {1} for {2}?",
                playerName, houseToBeBougth, BuyHouse[houseToBeBougth]));


            waitDecideBuyLock.WaitOne();
            if (buy)
            {
                p[playerIndex].PlayerDeposit -= (short)cost;
                BankDeposit += cost;

                showHouseOwner(playerIndex, houseToBeBougth);

                HouseOwner[houseToBeBougth] = playerIndex;

                if (HouseColors[houseToBeBougth] == Color.DodgerBlue)
                    p[playerIndex].OwnedBlueHouses += RentHouse[houseToBeBougth];

                else if (HouseColors[houseToBeBougth] == Color.Green)
                    p[playerIndex].OwnedGreenHouses += RentHouse[houseToBeBougth];

                else if (HouseColors[houseToBeBougth] == Color.Red)
                    p[playerIndex].OwnedRedHouses += RentHouse[houseToBeBougth];

                else if (HouseColors[houseToBeBougth] == Color.Yellow)
                    p[playerIndex].OwnedYellowHouses += RentHouse[houseToBeBougth];


            }

            changeGroupBuyButtons(false, BackColor,
                BackColor, string.Format("Buy?"));

        }
        private void showHouseOwner(short player, short house)
        {
            switch (player)
            {
                case 0:
                    labelOwners0.Text += house.ToString() + "\n";
                    break;
                case 1:
                    labelOwners1.Text += house.ToString() + "\n";
                    break;
                case 2:
                    labelOwners2.Text += house.ToString() + "\n";
                    break;
                case 3:
                    labelOwners3.Text += house.ToString() + "\n";
                    break;
                default:
                    break;
            }

        }
        private void changeGroupBuyButtons(bool enable, Color buttonColor,
            Color groupBoxColor, string message)
        {
            groupBoxBuy.BackColor = groupBoxColor;

            buttonYesBuy.Enabled = enable;
            buttonNoBuy.Enabled = enable;

            buttonYesBuy.BackColor = buttonColor;
            buttonNoBuy.BackColor = buttonColor;

            labelShow5.Text = message;
        }
        private void rentHouse(short playerIndex, string playerName,
            short houseToBeBougth, string ownerName, short ownerNumber)
        {
            changeGroupBuyButtons(false, BackColor, Color.LightGray,
                            string.Format("Player {0} rented house {1} from {2} for {3}",
                            playerName, houseToBeBougth, ownerName, RentHouse[houseToBeBougth]));

            ushort cost = 0;

            if (HouseColors[houseToBeBougth] == Color.DodgerBlue)
                cost = p[ownerNumber].OwnedBlueHouses;
            else if (HouseColors[houseToBeBougth] == Color.Green)
                cost = p[ownerNumber].OwnedGreenHouses;
            else if (HouseColors[houseToBeBougth] == Color.Red)
                cost = p[ownerNumber].OwnedRedHouses;
            else if (HouseColors[houseToBeBougth] == Color.Yellow)
                cost = p[ownerNumber].OwnedYellowHouses;

            p[playerIndex].PlayerDeposit -= (short)cost;
            p[ownerNumber].PlayerDeposit += (short)RentHouse[houseToBeBougth];

            Thread.Sleep(5000);


            changeGroupBuyButtons(false, BackColor,
                BackColor, string.Format("Buy?"));

        }
        private void showBankDeposit(uint bank)
        {
            labelBank.Text = bank.ToString();
        }
        // print the game result for user
        private void writeResultOfPlayers(string[] names)
        {
            int[] points = new int[NumberOfPlayers];

            for (short i = 0; i < NumberOfPlayers; i++)
            {
                switch (i)
                {
                    case 0:
                        labelOwners0.BackColor = Color.DodgerBlue;
                        break;
                    case 1:
                        labelOwners1.BackColor = Color.Green;
                        break;
                    case 2:
                        labelOwners2.BackColor = Color.Red;
                        break;
                    case 3:
                        labelOwners3.BackColor = Color.Yellow;
                        break;
                    default:
                        break;
                }
            }
            for (short i = 0; i < NumberOfPlayers; i++)
                points[i] = p[i].PlayerDeposit;
            labelPlayers.Text = string.Format(
                "{0}: {1}\n" +
                "{2}: {3}\n",
                names[0], points[0],
                names[1], points[1]);
            if (NumberOfPlayers > 2)
            {
                labelPlayers.Text += string.Format(
                "{0}: {1}\n",
                names[2], points[2]);
                if (NumberOfPlayers > 3)
                    labelPlayers.Text += string.Format(
                        "{0}: {1}\n",
                        names[3], points[3]);
            }
        }
        // colorize the 40 houses
        private void paintHouses()
        {
            for (int number = 1; number <= NumberOfHouses; number++)
            {
                switch (number)
                {
                    case 1:
                        label1.BackColor = HouseColors[number];
                        break;
                    case 2:
                        label2.BackColor = HouseColors[number];
                        break;
                    case 3:
                        label3.BackColor = HouseColors[number];
                        break;
                    case 4:
                        label4.BackColor = HouseColors[number];
                        break;
                    case 5:
                        label5.BackColor = HouseColors[number];
                        break;
                    case 6:
                        label6.BackColor = HouseColors[number];
                        break;
                    case 7:
                        label7.BackColor = HouseColors[number];
                        break;
                    case 8:
                        label8.BackColor = HouseColors[number];
                        break;
                    case 9:
                        label9.BackColor = HouseColors[number];
                        break;
                    case 10:
                        label10.BackColor = HouseColors[number];
                        break;
                    case 11:
                        label11.BackColor = HouseColors[number];
                        break;
                    case 12:
                        label12.BackColor = HouseColors[number];
                        break;
                    case 13:
                        label13.BackColor = HouseColors[number];
                        break;
                    case 14:
                        label14.BackColor = HouseColors[number];
                        break;
                    case 15:
                        label15.BackColor = HouseColors[number];
                        break;
                    case 16:
                        label16.BackColor = HouseColors[number];
                        break;
                    case 17:
                        label17.BackColor = HouseColors[number];
                        break;
                    case 18:
                        label18.BackColor = HouseColors[number];
                        break;
                    case 19:
                        label19.BackColor = HouseColors[number];
                        break;
                    case 20:
                        label20.BackColor = HouseColors[number];
                        break;
                    case 21:
                        label21.BackColor = HouseColors[number];
                        break;
                    case 22:
                        label22.BackColor = HouseColors[number];
                        break;
                    case 23:
                        label23.BackColor = HouseColors[number];
                        break;
                    case 24:
                        label24.BackColor = HouseColors[number];
                        break;
                    case 25:
                        label25.BackColor = HouseColors[number];
                        break;
                    case 26:
                        label26.BackColor = HouseColors[number];
                        break;
                    case 27:
                        label27.BackColor = HouseColors[number];
                        break;
                    case 28:
                        label28.BackColor = HouseColors[number];
                        break;
                    case 29:
                        label29.BackColor = HouseColors[number];
                        break;
                    case 30:
                        label30.BackColor = HouseColors[number];
                        break;
                    case 31:
                        label31.BackColor = HouseColors[number];
                        break;
                    case 32:
                        label32.BackColor = HouseColors[number];
                        break;
                    case 33:
                        label33.BackColor = HouseColors[number];
                        break;
                    case 34:
                        label34.BackColor = HouseColors[number];
                        break;
                    case 35:
                        label35.BackColor = HouseColors[number];
                        break;
                    case 36:
                        label36.BackColor = HouseColors[number];
                        break;
                    case 37:
                        label37.BackColor = HouseColors[number];
                        break;
                    case 38:
                        label38.BackColor = HouseColors[number];
                        break;
                    case 39:
                        label39.BackColor = HouseColors[number];
                        break;
                    case 40:
                        label40.BackColor = HouseColors[number];
                        break;
                    default:
                        break;
                }
            }
        }
        private void colorizeDiceRoller(Color back, short numberOfDice)
        {
            /* now I am proud ofthis function and all functionalizing 
             * in my program cause it made my life a lot easier */
            labelShow2.BackColor = back;
            //labelDice.Text = numberOfDice.ToString();
            switch (numberOfDice)
            {
                case 1:
                    pictureBoxDice.Image = Image.FromFile("dice1.png");
                    break;
                case 2:
                    pictureBoxDice.Image = Image.FromFile("dice2.png");
                    break;
                case 3:
                    pictureBoxDice.Image = Image.FromFile("dice3.png");
                    break;
                case 4:
                    pictureBoxDice.Image = Image.FromFile("dice4.png");
                    break;
                case 5:
                    pictureBoxDice.Image = Image.FromFile("dice5.png");
                    break;
                case 6:
                    pictureBoxDice.Image = Image.FromFile("dice6.png");
                    break;
                default:
                    break;
            }
        }
        private short rollTheDice(Color back)
        {
            changeButtonDice(true, Color.SaddleBrown);
            rollDiceLock.WaitOne();

            Random generateRandom = new Random(DateTime.Now.Second);
            short result = 0;


            diceSound.Play(); // dice sound


            for (int i = 0; i < 100; i++)
            {
                result = (short)generateRandom.Next(1, 7);
                colorizeDiceRoller(back, result);
                Thread.Sleep(8);
            }
            return result;
        }
        private void changeButtonDice(bool enable, Color color)
        {
            buttonRollTheDice.BackColor = color;
            buttonRollTheDice.Enabled = enable;
        }
        private void buttonRollTheDice_Click(object sender, EventArgs e)
        {
            try
            {
                changeButtonDice(false, BackColor);
                rollDiceLock.Release();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void buttonYesBuy_Click(object sender, EventArgs e)
        {
            buy = true;
            waitDecideBuyLock.Release();
        }
        private void buttonNoBuy_Click(object sender, EventArgs e)
        {
            buy = false;
            waitDecideBuyLock.Release();
        }
        private void showNamesAndPrices()
        {
            for (int number = 1; number <= NumberOfHouses; number++)
            {
                switch (number)
                {
                    case 1:
                        label1.Text =
                            string.Format("Furniture\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 2:
                        label2.Text =
                            string.Format("Depart.\n") +
                        string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 3:
                        label3.Text =
                            string.Format("Fish\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 4:
                        label4.Text =
                            string.Format("Grocery\n") +
                        string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 5:
                        label5.Text =
                            string.Format("Shoe\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 6:
                        label6.Text =
                            string.Format("Dairy\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 7:
                        label7.Text =
                            string.Format("Bonus\n");
                        break;
                    case 8:
                        label8.Text =
                            string.Format("Barber\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 9:
                        label9.Text =
                            string.Format("Move back 4 houses!\n");
                        break;
                    case 10:
                        label10.Text =
                            string.Format("Florist\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 11:
                        label11.Text =
                            string.Format("Telephone\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 12:
                        label12.Text =
                            string.Format("Airline\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 13:
                        label13.Text =
                            string.Format("Taxi\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 14:
                        label14.Text =
                            string.Format("Test luck!\n");
                        break;
                    case 15:
                        label15.Text =
                            string.Format("Railroad\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 16:
                        label16.Text =
                            string.Format("Ooops!\n");
                        break;
                    case 17:
                        label17.Text =
                            string.Format("Busline!\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 18:
                        label18.Text =
                            string.Format("Circus\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 19:
                        label19.Text =
                            string.Format("Book\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 20:
                        label20.Text =
                            string.Format("Move forward 5 houses!\n");
                        break;
                    case 21:
                        label21.Text =
                            string.Format("Test luck!\n");
                        break;
                    case 22:
                        label22.Text =
                            string.Format("Baseball\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 23:
                        label23.Text =
                            string.Format("Magazine\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 24:
                        label24.Text =
                            string.Format("Steel\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 25:
                        label25.Text =
                            string.Format("Tire\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 26:
                        label26.Text =
                            string.Format("Electric\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 27:
                        label27.Text =
                            string.Format("Television\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 28:
                        label28.Text =
                            string.Format("Diamond\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 29:
                        label29.Text =
                            string.Format("Watch\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 30:
                        label30.Text =
                            string.Format("Oil\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 31:
                        label31.Text =
                            string.Format("Farm\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 32:
                        label32.Text =
                            string.Format("Test luck!\n");
                        break;
                    case 33:
                        label33.Text =
                            string.Format("Orange\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 34:
                        label34.Text =
                            string.Format("Plantation\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 35:
                        label35.Text =
                            string.Format("Journal\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 36:
                        label36.Text =
                            string.Format("Ooops!\n");
                        break;
                    case 37:
                        label37.Text =
                            string.Format("Radio\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 38:
                        label38.Text =
                            string.Format("Car\n") +
                            string.Format("Buy: {0}\nRent: {1}",
                            BuyHouse[number], RentHouse[number]);
                        break;
                    case 39:
                        label39.Text =
                            string.Format("Move back 3 houses!\n");
                        break;
                    case 40:
                        label40.Text =
                            string.Format("Bonus\n");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}