using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace rich_uncle
{
    class GlobalVariables
    {
        // only one player can move at the same time
        public static Semaphore allowToMove = new Semaphore(1, 1);
        public static short rollTheDice()
        {
            Random generateRandom = new Random(DateTime.Now.Second);
            return (short)generateRandom.Next(1, 7);
        }
    }
}
