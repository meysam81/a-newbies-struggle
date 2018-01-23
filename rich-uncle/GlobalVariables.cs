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
        // ================================== class private fields ==========================================
        // change if you feel like it, be careful to know what you are doing
        private static ushort lowerBoundOfBuyingHouses = 100,
            upperBoundOfBuyingHouses = 1200;
        // again, change if you like, BE CAREFUL though!
        private static ushort lowerBoundOfRentingHouses = 10,
            upperBoundOfRentingHouses = 150;
        // DON'T change this one please
        private static ushort numberOfHouses = 40;

        private static Color[] houseColors;
        private static short[] buyHouse;
        private static short[] rentHouse;

        private static short initX = 7, initY = 415;
        
        
        // ==================================== class public property =======================================
        public static int NumberOfPlayers { get; set; }
        public static uint BankDeposit { get; set; }
        public static int PlayersInitialValue { get; set; }
        public static ushort NumberOfHouses
        {
            get
            {
                return numberOfHouses;
            }
        }


        // ================================== class public functions ========================================
        public GlobalVariables(FormMain form, out Color[] houseColours)
        {
            buyHouse = new short[NumberOfHouses];
            rentHouse = new short[NumberOfHouses];
            houseColors = new Color[NumberOfHouses + 1]; // ignore 0, go through 1-40
            Random rnd = new Random(DateTime.Now.Second);
            for (int i = 0; i < NumberOfHouses; i++)
            {
                buyHouse[i] = (short)rnd.Next(lowerBoundOfBuyingHouses, upperBoundOfBuyingHouses + 1);
                rentHouse[i] = (short)rnd.Next(lowerBoundOfRentingHouses, upperBoundOfRentingHouses + 1);
            }


            // now color the houses
            Color[] c = { Color.Blue, Color.Green, Color.Red, Color.Yellow };
            short blues = 0,
                reds = 0,
                greens = 0,
                yellows = 0;
            short counter = 1;
            while (blues < 10 || greens < 10 || reds < 10 || yellows < 10)
            {
                short saveRnd = (short)rnd.Next(0, 4);
                switch (saveRnd)
                {
                    case 0: // blue
                        if (blues < 10)
                        {
                            houseColors[counter++] = c[0];
                            blues++;
                        }
                        break;
                    case 1: // green
                        if (greens < 10)
                        {
                            houseColors[counter++] = c[1];
                            greens++;
                        }
                        break;
                    case 2: // red
                        if (reds < 10)
                        {
                            houseColors[counter++] = c[2];
                            reds++;
                        }
                        break;
                    case 3: // yellow
                        if (yellows < 10)
                        {
                            houseColors[counter++] = c[3];
                            yellows++;
                        }
                        break;
                    default:
                        break;
                }
            }
            //paintHouses(form);
            houseColours // form array
                = houseColors; // glv array
        }
        public static Point InitialPoisition
        {
            get
            {
                if (initY >= 475)
                    initY = 415;
                initY += 15;
                return new Point(initX, initY);
            }
        }

        // ====================================== public semaphore ==========================================
        // only one player can move at the same time
        public static Semaphore initPosLock = new Semaphore(1, 1);
        
    }
}
