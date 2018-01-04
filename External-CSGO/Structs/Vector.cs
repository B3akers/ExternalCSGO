using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleExternalCheatCSGO.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct struct_Vector
    {
        [FieldOffset(0x0)]
        public float x;
        [FieldOffset(0x4)]
        public float y;
        [FieldOffset(0x8)]
        public float z;

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", x, y, z);
        }
    }

    public class Vector
    {
        #region Variables
        public float _x;
        public float _y;
        public float _z;
        #endregion
        #region Constructors
        public Vector(struct_Vector vec)
        {
            _x = vec.x;
            _y = vec.y;
            _z = vec.z;
        }
        public Vector(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public Vector(float x, float y)
        {
            _x = x;
            _y = y;
            _z = 0f;
        }
        public Vector(Vector vec)
        {
            _x = vec._x;
            _y = vec._y;
            _z = vec._z;
        }
        #endregion
        #region Operators
        public static Vector operator +(Vector _vec1, Vector _vec2)
        {
            return new Vector(_vec2._x + _vec1._x, _vec2._y + _vec1._y, _vec2._z + _vec1._z);
        }
        public static Vector operator -(Vector _vec1, Vector _vec2)
        {
            return new Vector(_vec1._x - _vec2._x, _vec1._y - _vec2._y, _vec1._z - _vec2._z);
        }
        public static Vector operator *(Vector _vec1, float _f)
        {
            return new Vector(_vec1._x * _f, _vec1._y * _f, _vec1._z * _f);
        }
        public static Vector operator /(Vector _vec1, float _f)
        {
            return new Vector(_vec1._x / _f, _vec1._y / _f, _vec1._z / _f);
        }
        #endregion
        #region Functions
        public double Lenght2D()
        {
            return Math.Sqrt(_x * _x + _y * _y);
        }
        public double Lenght()
        {
            return Math.Sqrt(_x * _x + _y * _y + _z * _z);
        }
        public double DistanceTo(Vector _to)
        {
            return (this - _to).Lenght();
        }
        public void Normalize()
        {
            float flLen = (float)this.Lenght();
            if (flLen == 0)
            {
                this._x = 0;
                this._y = 0;
                this._z = 1;
                return;
            }
            flLen = 1 / flLen;
            this._x *= flLen;
            this._y *= flLen;
            this._z *= flLen;
        }
        public void NormalizeAngles()
        {
            if (this._x > 89)
                this._x = 89;
            if (this._x < -89)
                this._x = -89;

            while (_y > 180.0f)
                _y -= 360.0f;
            while (_y < -180.0f)
                _y += 360.0f;

            if (this._y > 180)
                this._y = 180;
            if (this._y < -180)
                this._y = -180;

        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", _x, _y, _z);
        }

        public struct_Vector ToStruct()
        {
            struct_Vector src = default(struct_Vector);
            src.x = this._x;
            src.y = this._y;
            src.z = this._z;
            return src;
        }

        #endregion
    }
}
