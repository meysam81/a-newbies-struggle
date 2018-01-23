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
        private enum direction { UP, RIGHT, DOWN, LEFT };
        private short posX, posY;
        private short widthX, widthY;
        private Graphics g;
        private Brush moveColor, formColor;
        private short currentHouse;
        private short ownedRedHouses, ownedBlueHouses,
            ownedYellowHouses, ownedGreenHouses;
        FormMain mainForm; // to get the house location
        public short NumberOfMovements// after rolling the dice
        {
            get
            {
                return NumberOfMovements;
            }
            set
            {
                if (value >= 1 && value <= 6)
                    NumberOfMovements = value;
                else
                    throw new Exception("Dice must be between 1-6 inclusively");
            }
        }
        public Player(FormMain mainForm, Color moveColor,
            short widthX = 10, short widthY = 10)
        {
            this.mainForm = mainForm;
            g = mainForm.CreateGraphics();
            MoveColor = moveColor;
            this.moveColor = new SolidBrush(moveColor);
            formColor = new SolidBrush(mainForm.BackColor);
            this.widthX = widthX;
            this.widthY = widthY;
            currentHouse = 0; // not playing just yet

            // not owning any house yet
            ownedBlueHouses = ownedGreenHouses = ownedRedHouses = ownedYellowHouses = 0;
        }
        public void initialPoisitioning()
        {
            initPosLock.WaitOne();
            Point curr = InitialPoisition; // using the property in glv
            initPosLock.Release();
            posX = (short)curr.X;
            posY = (short)curr.Y;
            g.FillEllipse(moveColor, posX, posY, widthX, widthY);
        }
        public void startPlaying()
        {
            initialPoisitioning(); // get ready to play!
            while (true)
            {
                Thread.CurrentThread.Suspend(); // wait for wake up call (wait for TURN)
                Point dest;
                if (currentHouse == 0) // before the game starts
                {
                    dest = mainForm.getHouseLocation(2); // CHANGE number 2
                    move((short)(posX + 11), posY, direction.RIGHT);
                    if (posY < 445)
                        move(posX, 445, direction.DOWN);
                    else
                        move(posX, 445, direction.UP);
                    if (posX < 560)
                        move(560, posY, direction.RIGHT);
                    if (posY > 340)
                        move(posX, 335, direction.UP);
                    if (posX > 15)
                        move(15, posY, direction.LEFT);
                    if (posY > 235)
                        move(posX, 225, direction.UP);
                    if (posX < 560)
                        move(560, posY, direction.RIGHT);
                    if (posY > 115)
                        move(posX, 115, direction.UP);
                    if (posX > 15)
                        move(15, posY, direction.LEFT);
                    if (posY > 5)
                        move(posX, 5, direction.UP);
                    if (posX < 560)
                        move(560, posY, direction.RIGHT);
                    if (posY < 445)
                        move(posX, 445, direction.DOWN);
                    if (posX > 15)
                        move(15, posY, direction.LEFT);

                }
                else if (currentHouse + NumberOfMovements > 40) // another round of play
                {
                    dest = mainForm.getHouseLocation((short)((NumberOfMovements + currentHouse) %
                        NumberOfHouses));
                }
                else // still on the same round
                {
                    dest = mainForm.getHouseLocation((short)((NumberOfMovements + currentHouse)));
                }
            }
        }
        public Color MoveColor { set; get; }

        // helper function for the class itself, not going to be used outside
        private void move(short destinationX, short destinationY, direction playerDirection)
        { // I am proud of this function

            g.FillEllipse(new SolidBrush(Color.BlueViolet), posX, posY, widthX, widthY);
            Thread.Sleep(300);
            switch (playerDirection) // rotate clockwise from top
            {
                case direction.UP: // up
                    while (posY > destinationY)
                    {
                        // clean current  position
                        g.FillEllipse(formColor, posX, posY, widthX, widthY);

                        posY -= 2;

                        // repaint in the next place
                        g.FillEllipse(moveColor, posX, posY, widthX, widthY);

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
                        g.FillEllipse(moveColor, posX, posY, widthX, widthY);

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
                        g.FillEllipse(moveColor, posX, posY, widthX, widthY);

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
                        g.FillEllipse(moveColor, posX, posY, widthX, widthY);

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
