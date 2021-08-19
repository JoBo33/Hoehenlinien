using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContourMap
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector()
        {

        }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector operator +(Vector a) => a;
        public static Vector operator +(Vector a, Vector b)
        {
            if (a == null && b == null)
            {
                return null;
            }
            else if (a == null && b != null)
            {
                return b;
            }
            else if (a != null && b == null)
            {
                return a;
            }
            else
            {
                return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
            }
        }
        public static Vector operator -(Vector a)
        {
            if (a == null)
            {
                return null;
            }

            return new Vector(-a.X, -a.Y, -a.Z);
        }
        public static Vector operator -(Vector a, Vector b) => a + (-b);
        public static Vector operator *(Vector a, double b)
        {
            if (b == 0)
            {
                return null;
            }

            if (a == null)
            {
                return null;
            }

            return new Vector(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector operator *(double b, Vector a) => a * b;
        public static Vector operator /(Vector a, double b) => new Vector(a.X / b, a.Y / b, a.Z / b);

        public static bool Equals(Vector a, Vector b)
        {
            if(a.X != b.X || a.Y != b.Y || a.Z != b.Z)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static Vector CrossProduct(Vector a, Vector b)
        {
            Vector cross = new Vector();
            cross.X = a.Y * b.Z - a.Z * b.Y;
            cross.Y = a.Z * b.X - a.X * b.Z;
            cross.Z = a.X * b.Y - a.Y * b.X;
            return cross;
        }

        public static double ScalarProduct(Vector a, Vector b)
        {
            double product = a.X * b.X + a.Y * b.Y + a.Z * b.Z;
            return product;
        }
    }
}
