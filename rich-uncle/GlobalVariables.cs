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
        // change if you feel like it
        private static ushort lowerBoundOfBuyingHouses = 100,
            upperBoundOfBuyingHouses = 1200;
        // again, change if you like
        private static ushort lowerBoundOfRentingHouses = 10,
            upperBoundOfRentingHouses = 150;


        // DON'T change this one please
        private static ushort numberOfHouses = 40;
        public static ushort NumberOfHouses
        {
            get
            {
                return numberOfHouses;
            }
        }

        private static Color[] houseColors;

        private static short[] buyHouse;

        private static short[] rentHouse;

        public GlobalVariables(FormMain form)
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
            paintHouses(form);
        }

        private void paintHouses(FormMain form)
        {
            for (int number = 1; number <= NumberOfHouses; number++)
            {
                switch (number)
                {
                    case 1:
                        form.label1.BackColor = houseColors[number];
                        break;
                    case 2:
                        form.label2.BackColor = houseColors[number];
                        break;
                    case 3:
                        form.label3.BackColor = houseColors[number];
                        break;
                    case 4:
                        form.label4.BackColor = houseColors[number];
                        break;
                    case 5:
                        form.label5.BackColor = houseColors[number];
                        break;
                    case 6:
                        form.label6.BackColor = houseColors[number];
                        break;
                    case 7:
                        form.label7.BackColor = houseColors[number];
                        break;
                    case 8:
                        form.label8.BackColor = houseColors[number];
                        break;
                    case 9:
                        form.label9.BackColor = houseColors[number];
                        break;
                    case 10:
                        form.label10.BackColor = houseColors[number];
                        break;
                    case 11:
                        form.label11.BackColor = houseColors[number];
                        break;
                    case 12:
                        form.label12.BackColor = houseColors[number];
                        break;
                    case 13:
                        form.label13.BackColor = houseColors[number];
                        break;
                    case 14:
                        form.label14.BackColor = houseColors[number];
                        break;
                    case 15:
                        form.label15.BackColor = houseColors[number];
                        break;
                    case 16:
                        form.label16.BackColor = houseColors[number];
                        break;
                    case 17:
                        form.label17.BackColor = houseColors[number];
                        break;
                    case 18:
                        form.label18.BackColor = houseColors[number];
                        break;
                    case 19:
                        form.label19.BackColor = houseColors[number];
                        break;
                    case 20:
                        form.label20.BackColor = houseColors[number];
                        break;
                    case 21:
                        form.label21.BackColor = houseColors[number];
                        break;
                    case 22:
                        form.label22.BackColor = houseColors[number];
                        break;
                    case 23:
                        form.label23.BackColor = houseColors[number];
                        break;
                    case 24:
                        form.label24.BackColor = houseColors[number];
                        break;
                    case 25:
                        form.label25.BackColor = houseColors[number];
                        break;
                    case 26:
                        form.label26.BackColor = houseColors[number];
                        break;
                    case 27:
                        form.label27.BackColor = houseColors[number];
                        break;
                    case 28:
                        form.label28.BackColor = houseColors[number];
                        break;
                    case 29:
                        form.label29.BackColor = houseColors[number];
                        break;
                    case 30:
                        form.label30.BackColor = houseColors[number];
                        break;
                    case 31:
                        form.label31.BackColor = houseColors[number];
                        break;
                    case 32:
                        form.label32.BackColor = houseColors[number];
                        break;
                    case 33:
                        form.label33.BackColor = houseColors[number];
                        break;
                    case 34:
                        form.label34.BackColor = houseColors[number];
                        break;
                    case 35:
                        form.label35.BackColor = houseColors[number];
                        break;
                    case 36:
                        form.label36.BackColor = houseColors[number];
                        break;
                    case 37:
                        form.label37.BackColor = houseColors[number];
                        break;
                    case 38:
                        form.label38.BackColor = houseColors[number];
                        break;
                    case 39:
                        form.label39.BackColor = houseColors[number];
                        break;
                    case 40:
                        form.label40.BackColor = houseColors[number];
                        break;
                    default:
                        break;
                }
            }
        }

        private static short initX = 7, initY = 415;
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
        // only one player can move at the same time
        public static Semaphore initPosLock = new Semaphore(1, 1);
        public static short rollTheDice()
        {
            Random generateRandom = new Random(DateTime.Now.Second);
            return (short)generateRandom.Next(1, 7);
        }
    }
}
