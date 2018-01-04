using SimpleExternalCheatCSGO.SDK;
using SimpleExternalCheatCSGO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExternalCheatCSGO.Util
{
    public static class MathUtil
    {
        public static float DotProduct(Vector a, Vector b)
        {
            return (a._x * b._x + a._y * b._y + a._z * b._z);
        }

        public static Vector VectorTransform(Vector in1, matrix3x4 in2)
        {
            Vector vecOut = new Vector(0, 0, 0);
            vecOut._x = DotProduct(in1, new Vector(in2.first[0].second[0], in2.first[0].second[1], in2.first[0].second[2])) + in2.first[0].second[3];
            vecOut._y = DotProduct(in1, new Vector(in2.first[1].second[0], in2.first[1].second[1], in2.first[1].second[2])) + in2.first[1].second[3];
            vecOut._z = DotProduct(in1, new Vector(in2.first[2].second[0], in2.first[2].second[1], in2.first[2].second[2])) + in2.first[2].second[3];
            return vecOut;
        }

        public static double DegreeToRadian(float angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static Vector CalcAngle(Vector src, Vector dst)
        {
            Vector delta = new Vector((src._x - dst._x), (src._y - dst._y), (src._z - dst._z));
            double hyp = Math.Sqrt(delta._x * delta._x + delta._y * delta._y);
            Vector angles = new Vector(0, 0, 0);
            angles._x = (float)(Math.Atan(delta._z / hyp) * 57.295779513082f);
            angles._y = (float)(Math.Atan(delta._y / delta._x) * 57.295779513082f);
            angles._z = 0.0f;
            if (delta._x >= 0.0)
                angles._y += 180.0f;
            return angles;
        }

        public static Vector AngleVectors(Vector angles)
        {
            float sp, sy, cp, cy;

            sy = (float)Math.Sin(DegreeToRadian(angles._y));
            cy = (float)Math.Cos(DegreeToRadian(angles._y));

            sp = (float)Math.Sin(DegreeToRadian(angles._x));
            cp = (float)Math.Cos(DegreeToRadian(angles._x));

            return new Vector(cp * cy, cp * sy, -sp);
        }

        public const float MaxDegrees = 180.0f;

        public static float FovToPlayer(Vector ViewOffSet, Vector View, CBasePlayer pEntity, int aHitBox)
        {
            Vector Angles = View;
            Vector Origin = ViewOffSet;

            Vector Delta = new Vector(0, 0, 0);
            Vector Forward = new Vector(0, 0, 0);

            Forward = AngleVectors(Angles);

            Vector AimPos = pEntity.GetHitboxPosition(aHitBox);

            Delta = AimPos - Origin;

            Delta.Normalize();

            float Dot = DotProduct(Forward, Delta);
            return (float)(Math.Acos(Dot) * (MaxDegrees / Math.PI));
        }


    }
}
