using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;

namespace Shapes;

public abstract class Shape
{
    public double X, Y, Xd, Yd;
    protected bool Moving;

    public bool Move
    {
        get => Moving;
        set => Moving = value;
    }

    internal static int R { get; set; } = 20;
    public bool IsShell { get; set; }

    public static Shape newDot(double x, double y, stype type)
    {
        return type switch
        {
            stype.Circle => new Circle(x, y),
            stype.Triangle => new Triangle(x, y),
            stype.Square => new Square(x, y),
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
        Moving = false;
    }

    public Circle(double x, double y)
    {
        this.X = x;
        this.Y = y;
        Moving = false;
    }

    public override bool IsInside(double xMouse, double yMouse)
    {
        return Math.Pow(xMouse - X, 2) + Math.Pow(yMouse - Y, 2) <= Math.Pow(R, 2);
    }

    public override void Draw(DrawingContext drawingContext, Brush brush, Pen pen)
    {
        drawingContext.DrawEllipse(brush, pen, new Point(this.X, this.Y), R, R);
    }
}

public class Square : Shape
{
    public Square()
    {
        X = 540;
        Y = 360;
        Moving = false;
    }

    public Square(double x, double y)
    {
        this.X = x;
        this.Y = y;
        Moving = false;
    }

    public override bool IsInside(double xMouse, double yMouse)
    {
        return Math.Abs(X - xMouse) <= (R / Math.Sqrt(2)) && Math.Abs(Y - yMouse) <= (R / Math.Sqrt(2));
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
        Moving = false;
    }

    public Triangle(double x, double y)
    {
        X = x;
        Y = y;
        Moving = false;
    }

    public override bool IsInside(double xMouse, double yMouse)
    {
        double xcos = Math.Sqrt(3) / 2 * R;
        Point a = new Point(X + xcos, Y + R * 0.5);
        Point b = new Point(X, Y - R);
        Point c = new Point(X - xcos, Y + R * 0.5);
        Point p = new Point(xMouse, yMouse);

        double abc = TriangleArea(a, b, c);
        double abp = TriangleArea(a, b, p);
        double acp = TriangleArea(a, c, p);
        double bcp = TriangleArea(b, c, p);

        return Math.Abs(abc - (abp + acp + bcp)) < 0.0001;
    }

    private static double TriangleArea(Point a, Point b, Point c)
    {
        return Math.Abs((a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)) / 2.0);
    }

    public override void Draw(DrawingContext drawingContext, Brush brush, Pen pen)
    {
        double xcos = Math.Sqrt(3) / 2 * R;
        Point l = new Point(X + xcos, Y + R * 0.5);
        Point t = new Point(X, Y - R);
        Point r = new Point(X - xcos, Y + R * 0.5);
        drawingContext.DrawGeometry(brush, pen, new PolylineGeometry(new List<Point> { l, t, r }, true));
    }
}