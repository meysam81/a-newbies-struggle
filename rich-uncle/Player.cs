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
        private enum direction { UP, RIGHT, DOWN, LEFT};
        private short posX, posY;
        private short widthX, widthY;
        private Graphics g;
        private Brush moveColor, formColor;
        public Player(Form mainForm, Color moveColor, short posX, short posY,
            short widthX = 10, short widthY = 10)
        {
            g = mainForm.CreateGraphics();
            this.moveColor = new SolidBrush(moveColor);
            formColor = new SolidBrush(mainForm.BackColor);
            this.posX = posX;
            this.posY = posY;
            this.widthX = widthX;
            this.widthY = widthY;
        }
        public void startPlaying()
        {

        }

        // helper function for the class itself, not going to be used outside
        private void move(short destinationX, short destinationY, direction upOrRight)
        {
            switch (upOrRight) // rotate clockwise from top
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
