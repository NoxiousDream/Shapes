using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;

namespace Shapes;

public abstract class Shape
{
    public double X, Y, Xd, Yd;

    public bool Move { get; set; }
    internal static int R { get; set; } = 20;
    public bool IsShell { get; set; }

    public static Shape newDot(double x, double y, ShapeType type)
    {
        return type switch
        {
            ShapeType.Circle => new Circle(x, y),
            ShapeType.Triangle => new Triangle(x, y),
            ShapeType.Square => new Square(x, y),
            _ => throw new Exception("WTF")
        };
    }

    public abstract void Draw(DrawingContext drawingContext, Brush brush, Pen pen);

    public abstract bool IsInside(double x, double y);
}

public class Circle : Shape
{
    public Circle()
    {
        X = 540;
        Y = 360;
        Move = false;
    }

    public Circle(double x, double y)
    {
        X = x;
        Y = y;
        Move = false;
    }

    public override bool IsInside(double xMouse, double yMouse)
    {
        return Math.Pow(xMouse - X, 2) + Math.Pow(yMouse - Y, 2) <= Math.Pow(R, 2);
    }

    public override void Draw(DrawingContext drawingContext, Brush brush, Pen pen)
    {
        drawingContext.DrawEllipse(brush, pen, new Point(X, Y), R, R);
    }
}

public class Square : Shape
{
    public Square()
    {
        X = 540;
        Y = 360;
        Move = false;
    }

    public Square(double x, double y)
    {
        X = x;
        Y = y;
        Move = false;
    }

    public override bool IsInside(double xMouse, double yMouse)
    {
        return Math.Abs(X - xMouse) <= R / Math.Sqrt(2) && Math.Abs(Y - yMouse) <= R / Math.Sqrt(2);
    }

    public override void Draw(DrawingContext drawingContext, Brush brush, Pen pen)
    {
        drawingContext.DrawRectangle(brush, pen,
            new Rect(new Point(X - 0.75 * R, Y - 0.75 * R), new Size(1.5 * R, 1.5 * R)));
    }
}

public class Triangle : Shape
{
    public Triangle()
    {
        X = 540;
        Y = 360;
        Move = false;
    }

    public Triangle(double x, double y)
    {
        X = x;
        Y = y;
        Move = false;
    }

    public override bool IsInside(double xMouse, double yMouse)
    {
        var xcos = Math.Sqrt(3) / 2 * R;
        var a = new Point(X + xcos, Y + R * 0.5);
        var b = new Point(X, Y - R);
        var c = new Point(X - xcos, Y + R * 0.5);
        var p = new Point(xMouse, yMouse);

        var abc = TriangleArea(a, b, c);
        var abp = TriangleArea(a, b, p);
        var acp = TriangleArea(a, c, p);
        var bcp = TriangleArea(b, c, p);

        return Math.Abs(abc - (abp + acp + bcp)) < 0.0001;
    }

    private static double TriangleArea(Point a, Point b, Point c)
    {
        return Math.Abs((a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)) / 2.0);
    }

    public override void Draw(DrawingContext drawingContext, Brush brush, Pen pen)
    {
        var xcos = Math.Sqrt(3) / 2 * R;
        var l = new Point(X + xcos, Y + R * 0.5);
        var t = new Point(X, Y - R);
        var r = new Point(X - xcos, Y + R * 0.5);
        drawingContext.DrawGeometry(brush, pen, new PolylineGeometry(new List<Point> { l, t, r }, true));
    }
}