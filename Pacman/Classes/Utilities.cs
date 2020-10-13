using System;
using System.Drawing;
using System.Security.Cryptography;


namespace Pacman.Classes
{
    public static class Utilities
    {
        public static int ChooseRandomly(int from, int to)
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);

                int n = from + (Math.Abs(BitConverter.ToInt32(rno, 0)) % to);
                return n;
            }
        }

        public static int Heuristic(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static int Cost(Point a, Point b)
        {
            return Heuristic(a, b);   //A star algorithm uses distance between two points (because in pacman there is only this option), cost could be any other function in different games etc.
                                      // (for example cost of going through forest is higher than to goo through plain)
        }
    }
}
