using System;

namespace Psim
{
    namespace Geometry2D
    {
        public class Point
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Point(double x = 0, double y = 0)
            {
                X = x;
                Y = y;
            }

            public void GetCoords(out double x, out double y)
            {
                x = X;
                y = Y;
            }
            public void SetCoords(double? x, double? y)
            {
                X = x ?? X;
                Y = y ?? Y;
            }
        }
        public class Vector
        {
            public double DX { get; set; }
            public double DY { get; set; }

            public Vector(double dx = 0, double dy = 0)
            {
                Set(dx, dy);
            }

            public void Set(double dx, double dy)
            {
                bool InRange(double x) => x >= -1 && x <= 1;

                if(InRange(dx) && InRange(dy))
                {
                    DX = dx;
                    DY = dy;
                    return;
                }

                throw new ArgumentOutOfRangeException();

            }
            public override string ToString() => $"({DX}, {DY})";
        }
        public class Rectangle
        {
            public double Length { get; }
            public double Width { get; }
            public double Area { get; }

            public Rectangle(double length, double width)
            {
                Length = length;
                Width = width;
                Area = length * width;
            }
        }
    }
}
