using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static rich_uncle.GlobalVariables;

namespace rich_uncle
{
    class House
    {
        private int posX, posY, labelStartX;
        private ushort labelNumber;
        public House(Label label, ushort labelNumber)
        {
            posX = label.Location.X;
            posY = label.Location.Y;

            labelStartX = label.Location.X; // to rotate if posX is greater than end of label
            this.labelNumber = labelNumber;
        }

        // ==================================== class public property =======================================
        public Point HouseLocation
        {
            get
            {
                housesLock[labelNumber].WaitOne();
                Point result = new Point(posX, posY);
                posX += 15; // for gaps between 2 players in a same house
                if (posX >= (labelStartX + 45))
                    posX = labelStartX;
                housesLock[labelNumber].Release();
                return result;
            }
        }
    }
}
