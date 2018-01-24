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
        private static ushort numberOfHouses = 41;
        private static Color[] houseColors;
        private static ushort[] buyHouse;
        private static ushort[] rentHouse;
        private static short initX = 7, initY = 415;

        // ==================================== class public property =======================================
        public static int NumberOfPlayers { get; set; }
        public static ushort BankDeposit { get; set; }
        public static short PlayersInitialValue { get; set; }
        public static ushort NumberOfHouses
        {
            get
            {
                return numberOfHouses;
            }
        }
        public static Color[] HouseColors
        {
            get
            {
                return houseColors;
            }
        }
        public static ushort[] BuyHouse
        {
            get
            {
                return buyHouse;
            }
        }
        public static ushort[] RentHouse
        {
            get
            {
                return rentHouse;
            }
        }
        public static short[] HouseOwner { get; set; }

        // ================================== class public functions ========================================
        public GlobalVariables(FormMain form)
        {
            buyHouse = new ushort[NumberOfHouses + 1];
            rentHouse = new ushort[NumberOfHouses + 1];
            houseColors = new Color[NumberOfHouses + 1]; // ignore 0, go through 1-40
            Random rnd = new Random(DateTime.Now.Second);
            for (int i = 1; i <= NumberOfHouses; i++)
            {
                buyHouse[i] = (ushort)rnd.Next(lowerBoundOfBuyingHouses, upperBoundOfBuyingHouses + 1);
                rentHouse[i] = (ushort)rnd.Next(lowerBoundOfRentingHouses, upperBoundOfRentingHouses + 1);
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

            HouseOwner = new short[NumberOfHouses + 1];
            for (short i = 1; i <= NumberOfHouses; i++)
                HouseOwner[i] = -1;

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
        public static Semaphore rollDiceLock = new Semaphore(1, 1);

    }
}
