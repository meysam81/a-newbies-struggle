using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace rich_uncle
{
    class GlobalVariables
    {
        private static short initX = 7, initY = 415;
        public static Point InitialPoisition
        {
            get
            {
                initY += 15;
                return new Point(initX, initY);
            }
        }
        // only one player can move at the same time
        public static Semaphore initPosLock = new Semaphore(1, 1);
        public static short rollTheDice()
        {
            Random generateRandom = new Random(DateTime.Now.Second);
            return (short)generateRandom.Next(1, 7);
        }
    }
}
