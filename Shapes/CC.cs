using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Shapes;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class CustomControl : UserControl
{
    private List<Shape> _Shapes = new();
    private ShapeType _shapeType = ShapeType.Circle;
    private Color colour = new(255, 140, 255, 0);
    private Methods Method = Methods.ByDefenition;

    public void ChangeToCircle()
    {
        _shapeType = ShapeType.Circle;
    }

    public void ChangeToTriangle()
    {
        _shapeType = ShapeType.Triangle;
    }

    public void ChangeToSquare()
    {
        _shapeType = ShapeType.Square;
    }

    public void ChangeToByDefenition()
    {
        Method = Methods.ByDefenition;
    }

    public void ChangeToByGraham()
    {
        Method = Methods.ByGraham;
    }

    public void ChangeToByAndrew()
    {
        Method = Methods.ByAndrew;
    }

    public void LPointerPressed(double x, double y)
    {
        var creating = true;
        foreach (var shape in _Shapes.Where(shape => shape.IsInside(x, y)))
        {
            shape.Move = true;
            shape.Xd = shape.X - x;
            shape.Yd = shape.Y - y;
            creating = false;
        }

        if (creating) _Shapes.Add(Shape.newDot(x, y, _shapeType));
    }

    public void RPointerPressed(double x, double y)
    {
        for (var i = 0; i < _Shapes.Count; i++)
        {
            if (!_Shapes[i].IsInside(x, y)) continue;
            _Shapes.Remove(_Shapes[i]);
            break;
        }
    }

    public void Pointer_Moved(double X, double Y)
    {
        var update = false;
        foreach (var c in _Shapes)
            if (c.Move)
            {
                c.X = X + c.Xd;
                c.Y = Y + c.Yd;
                update = true;
            }

        if (update)
            InvalidateVisual();
    }

    public void PoiterReleased()
    {
        foreach (var c in _Shapes) c.Move = false;

        InvalidateVisual();
    }

    private void DrawShell(DrawingContext? drawingContext, IPen? pen, bool Draw)
    {
        foreach (var s in _Shapes)
            s.IsShell = false;

        for (var i = 0; i < _Shapes.Count - 1; i++)
        {
            var a = _Shapes[i];
            for (var j = i + 1; j < _Shapes.Count; j++)
            {
                var b = _Shapes[j];

                var k = (b.Y - a.Y) / (b.X - a.X);
                var d = a.Y - k * a.X;
                int above = 0, below = 0, ontheline = 0;

                for (var l = 0; l < _Shapes.Count; l++)
                {
                    var c = _Shapes[l];
                    if (l == i || l == j) continue;

                    if (!double.IsInfinity(k))
                    {
                        if (c.Y > k * c.X + d) below += 1;
                        else if (c.Y < k * c.X + d) above += 1;
                        else if (c.X > Math.Max(a.X, b.X) && c.X < Math.Min(a.X, b.X)) ontheline += 1;
                    }
                    else
                    {
                        if (c.X > a.X) below += 1;
                        else if (c.X < a.X) above += 1;
                        else if (c.X > Math.Max(a.X, b.X) && c.X < Math.Min(a.X, b.X)) ontheline += 1;
                    }
                }

                if (above * below + ontheline == 0)
                {
                    drawingContext.DrawLine(pen, new Point(a.X, a.Y), new Point(b.X, b.Y));
                    a.IsShell = true;
                    b.IsShell = true;
                }
            }
        }

        if (Draw)
        {
            // Delete Dots
            if (_Shapes.Any(s => s.Move)) return;

            for (var i = 0; i < _Shapes.Count; i++)
            {
                var s = _Shapes[i];
                if (!s.IsShell) _Shapes.Remove(s);
            }
        }
    }

    private void DrawGraham(DrawingContext? drawingContext, IPen? pen, bool Draw)
    {
        var p0 = _Shapes.OrderBy(p => p.Y).ThenBy(p => p.X).First();
        var sortedPoints = _Shapes.OrderBy(p => Math.Atan2(p.Y - p0.Y, p.X - p0.X)).ToList();
        var hull = new List<Shape> { sortedPoints[0], sortedPoints[1] };
        foreach (var p in sortedPoints)
        {
            while (hull.Count >= 2)
            {
                var a = hull.Skip(hull.Count - 2).Take(2).ToList();
                if (Orientation(a[0], a[1], p) < 0)
                    hull.RemoveAt(hull.Count - 1);
                else
                    break;
            }

            hull.Add(p);
        }

        if (Draw)
        {
            var D = true;
            foreach (var s in _Shapes)
                if (s.Move)
                    D = false;

            if (D)
                for (var i = 0; i < _Shapes.Count; i++)
                {
                    var s = _Shapes[i];
                    if (!hull.Contains(s) && !s.Move)
                    {
                        _Shapes.Remove(s);
                        i--;
                    }
                }

            for (var i = 0; i < hull.Count; i++)
                drawingContext.DrawLine(pen,
                    new Point(hull[i].X, hull[i].Y),
                    new Point(hull[(i + 1) % hull.Count].X, hull[(i + 1) % hull.Count].Y));
        }
    }

    private void DrawAndrew(DrawingContext? drawingContext, IPen? pen, bool Draw)
    {
        var sorted_points = _Shapes.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
        var l = new List<Shape>();
        foreach (var p in sorted_points)
        {
            while (l.Count >= 2 && Orientation(l[^2], l[^1], p) <= 0) l.RemoveAt(l.Count - 1);
            l.Add(p);
        }

        sorted_points.Reverse();
        var u = new List<Shape>();
        foreach (var p in sorted_points)
        {
            while (u.Count >= 2 && Orientation(u[^2], u[^1], p) <= 0) u.RemoveAt(u.Count - 1);
            u.Add(p);
        }

        if (Draw)
        {
            var D = true;
            foreach (var s in _Shapes)
                if (s.Move)
                    D = false;

            if (D)
                for (var i = 0; i < _Shapes.Count; i++)
                {
                    var s = _Shapes[i];
                    if (!l.Contains(s) && !u.Contains(s))
                    {
                        _Shapes.Remove(s);
                        i--;
                    }
                }

            for (var i = 0; i < l.Count - 1; i++)
                drawingContext.DrawLine(pen,
                    new Point(l[i].X, l[i].Y),
                    new Point(l[i + 1].X, l[(i + 1) % l.Count].Y));

            for (var i = 0; i < u.Count - 1; i++)
                drawingContext.DrawLine(pen,
                    new Point(u[i].X, u[i].Y),
                    new Point(u[i + 1].X, u[(i + 1) % u.Count].Y));
        }
    }

    private static double Orientation(Shape a, Shape b, Shape c)
    {
        return (b.X - a.X) * (c.Y - b.Y) - (b.Y - a.Y) * (c.X - b.X);
    }

    public override void Render(DrawingContext drawingContext)
    {
        var pen_for_outline = new Pen(Brushes.Black, 1.7, lineCap: PenLineCap.Round);

        var brush = new SolidColorBrush(colour);

        if (_Shapes.Count > 2)
            switch (Method)
            {
                case Methods.ByDefenition:
                    DrawShell(drawingContext, new Pen(Brushes.Cyan, 1.7, lineCap: PenLineCap.Round), true);
                    break;
                case Methods.ByGraham:
                    DrawGraham(drawingContext, new Pen(Brushes.Magenta, 1.7, lineCap: PenLineCap.Round), true);
                    break;
                case Methods.ByAndrew:
                    DrawAndrew(drawingContext, new Pen(Brushes.Yellow, 1.7, lineCap: PenLineCap.Round), true);
                    break;
            }

        foreach (var c in _Shapes) c.Draw(drawingContext, brush, pen_for_outline);
    }

    public void ChangeColor(Color color)
    {
        colour = color;
        InvalidateVisual();
    }

    public double CountTime(Methods method, List<Shape> shapes)
    {
        var start = DateTime.UtcNow;
        _Shapes = shapes;
        switch (method)
        {
            case Methods.ByDefenition:
                DrawShell(null, null, false);
                break;
            case Methods.ByGraham:
                DrawGraham(null, null, false);
                break;
            case Methods.ByAndrew:
                DrawAndrew(null, null, false);
                break;
        }

        return (DateTime.UtcNow - start).TotalMilliseconds;
    }
}