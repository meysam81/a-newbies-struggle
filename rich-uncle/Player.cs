using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static rich_uncle.GlobalVariables;

namespace rich_uncle
{
    class Player
    {
        // ================================== class private fields ==========================================
        private enum direction { UP, RIGHT, DOWN, LEFT };
        private short posX, posY;
        private short widthX, widthY;
        private Graphics g;
        private Brush moveBrush, formColor;
        private short currentHouse;
        FormMain mainForm; // to get the house location
        private short numberOfMovements;
        private short threadNumber;
        
        // ==================================== class public property =======================================
        public short
            OwnedBlueHouses,
            OwnedGreenHouses,
            OwnedRedHouses,
            OwnedYellowHouses;
        public short NumberOfMovements// after rolling the dice
        {
            get
            {
                return numberOfMovements;
            }
            set
            {
                if (value >= 1 && value <= 6)
                    numberOfMovements = value;
                else
                    throw new Exception("Dice must be between 1-6 inclusively");
            }
        }
        public Color MoveColor { set; get; }
        public short PlayerDeposit { get; set; }
        public short CurrentHouse
        {
            get
            {
                return currentHouse;
            }
            set
            {
                currentHouse = value;
            }
        }

        // =================================== class public functions =======================================
        public Player(FormMain mainForm, Color moveColor, short threadNumber,
            short widthX = 10, short widthY = 10)
        {
            PlayerDeposit = PlayersInitialValue;
            this.threadNumber = threadNumber;
            this.mainForm = mainForm;
            g = mainForm.CreateGraphics();
            MoveColor = moveColor;
            this.moveBrush = new SolidBrush(moveColor);
            formColor = new SolidBrush(mainForm.BackColor);
            this.widthX = widthX;
            this.widthY = widthY;
            CurrentHouse = 0; // not playing just yet

            // not owning any house yet
            OwnedBlueHouses = OwnedGreenHouses = OwnedRedHouses = OwnedYellowHouses = 0;
        }

        public void initialPoisitioning()
        {
            initPosLock.WaitOne();
            Point curr = InitialPoisition; // using the property in glv
            initPosLock.Release();
            posX = (short)curr.X;
            posY = (short)curr.Y;
            g.FillEllipse(moveBrush, posX, posY, widthX, widthY);
        }

        public void startPlaying()
        {
            initialPoisitioning(); // get ready to play!
            while (true)
            {
                Thread.CurrentThread.Suspend(); // wait for wake up call (wait for TURN)
                Point dest;
                if (CurrentHouse == 0) // before the game starts
                {
                    CurrentHouse = numberOfMovements; // now we are at a new position
                    


                    dest = mainForm.getHouseLocation(CurrentHouse);


                    move((short)(posX + 11), posY, direction.RIGHT);
                    move(posX, 445, direction.DOWN);
                    move(posX, 445, direction.UP);
                    move((short)(dest.X + 25), posY, direction.RIGHT);
                    move(posX, (short)(dest.Y - 10), direction.DOWN);



                    //if (posY > 340)
                    //    move(posX, 335, direction.UP);
                    //if (posX > 15)
                    //    move(15, posY, direction.LEFT);
                    //if (posY > 235)
                    //    move(posX, 225, direction.UP);
                    //if (posX < 560)
                    //    move(560, posY, direction.RIGHT);
                    //if (posY > 115)
                    //    move(posX, 115, direction.UP);
                    //if (posX > 15)
                    //    move(15, posY, direction.LEFT);
                    //if (posY > 5)
                    //    move(posX, 5, direction.UP);
                    //if (posX < 560)
                    //    move(560, posY, direction.RIGHT);
                    //if (posY < 445)
                    //    move(posX, 445, direction.DOWN);
                    //if (posX > 15)
                    //    move(15, posY, direction.LEFT);

                }
                else if (CurrentHouse + NumberOfMovements > 40) // another round of play
                {
                    CurrentHouse += numberOfMovements; // now we are at a new position
                    CurrentHouse %= 40;

                    dest = mainForm.getHouseLocation(CurrentHouse);

                    move(posX, 5, direction.UP);
                    move(560, posY, direction.RIGHT);
                    move(posX, 445, direction.DOWN);
                    move((short)(dest.X + 25), posY, direction.LEFT);
                    move(posX, (short)(dest.Y - 10), direction.DOWN);
                }
                else // still on the same round
                {
                    CurrentHouse += numberOfMovements; // now we are at a new position

                    dest = mainForm.getHouseLocation(CurrentHouse);

                    if (dest.Y < posY)
                    {
                        short toGoLeft = (short)(posX - 15), toGoRight = (short)(560 - posX);
                        if (toGoLeft > toGoRight)
                        {
                            move(posX, (short)(posY - 20), direction.UP);
                            move(560, posY, direction.RIGHT);
                            move(posX, (short)(dest.Y - 25), direction.UP);
                            move((short)(dest.X + 25), posY, direction.LEFT);
                            move(posX, (short)(dest.Y - 10), direction.DOWN);
                        }
                        else
                        {
                            move(posX, (short)(posY - 20), direction.UP);
                            move(15, posY, direction.LEFT);
                            move(posX, (short)(dest.Y - 25), direction.UP);
                            move((short)(dest.X + 25), posY, direction.RIGHT);
                            move(posX, (short)(dest.Y - 10), direction.DOWN);
                        }
                    }
                    else
                    {
                        move(posX, (short)(posY - 20), direction.UP);
                        move((short)(dest.X + 25), posY, direction.LEFT);
                        move((short)(dest.X + 25), posY, direction.RIGHT);
                        move(posX, (short)(dest.Y - 10), direction.DOWN);
                    }
                }
            }
        }
        
        // =================================== class private functions ======================================
        // helper function for the class itself, not going to be used outside
        private void move(short destinationX, short destinationY, direction playerDirection)
        { // I am proud of this function

            switch (playerDirection) // rotate clockwise from top
            {
                case direction.UP: // up
                    while (posY > destinationY)
                    {
                        // clean current  position
                        g.FillEllipse(formColor, posX, posY, widthX, widthY);

                        posY -= 2;

                        // repaint in the next place
                        g.FillEllipse(moveBrush, posX, posY, widthX, widthY);

                        // sleep, for the human eye to see the movement and NOT the jumping
                        Thread.Sleep(8); // in milliseconds
                    }
                    break;
                case direction.RIGHT: // right
                    while (posX < destinationX)
                    {
                        // clean current  position
                        g.FillEllipse(formColor, posX, posY, widthX, widthY);

                        posX += 2;

                        // repaint in the next place
                        g.FillEllipse(moveBrush, posX, posY, widthX, widthY);

                        // sleep, for the human eye to see the movement and NOT the jumping
                        Thread.Sleep(8); // in milliseconds
                    }
                    break;
                case direction.DOWN: // down
                    while (posY < destinationY)
                    {
                        // clean current  position
                        g.FillEllipse(formColor, posX, posY, widthX, widthY);

                        posY += 2;

                        // repaint in the next place
                        g.FillEllipse(moveBrush, posX, posY, widthX, widthY);

                        // sleep, for the human eye to see the movement and NOT the jumping
                        Thread.Sleep(8); // in milliseconds
                    }
                    break;
                case direction.LEFT: // left
                    while (posX > destinationX)
                    {
                        // clean current  position
                        g.FillEllipse(formColor, posX, posY, widthX, widthY);

                        posX -= 2;

                        // repaint in the next place
                        g.FillEllipse(moveBrush, posX, posY, widthX, widthY);

                        // sleep, for the human eye to see the movement and NOT the jumping
                        Thread.Sleep(8); // in milliseconds
                    }
                    break;

                default:
                    break;
            }
        }

    }
}
