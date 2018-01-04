using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.Structs
{
    public class Color
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color()
        {
            R = 0;
            G = 0;
            B = 0;
            A = 255;
        }
        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }
        public Color(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", (int)R, (int)G, (int)B, (int)A);
        }
    }
}
