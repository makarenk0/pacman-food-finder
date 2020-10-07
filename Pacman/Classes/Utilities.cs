using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
    }
}
