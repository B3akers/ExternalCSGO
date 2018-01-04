using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.Util
{
    public static class RandomHelper
    {
        public static int RandomInt(int min,int max)
        {
            Random rand = new Random(Environment.TickCount);
            return rand.Next(min, max);
        }

        public static string RandomString(int length)
        {
            Random rand = new Random(Environment.TickCount);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
    }
}
