using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Shapes
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class CustomControl : UserControl
    {
        private readonly List<Shape> Shapes = new();
        private stype Stype = stype.Circle;

        public void ChangeToCircle()
        {
            Stype = stype.Circle;
        }

        public void ChangeToTriangle()
        {
            Stype = stype.Triangle;
        }

        public void ChangeToSquare()
        {
            Stype = stype.Square;
        }

        public void LPointerPressed(double x, double y)
        {
            var creating = true;
            foreach (var shape in Shapes.Where(shape => shape.IsInside(x, y)))
            {
                shape.Move = true;
                shape.Xd = shape.X - x;
                shape.Yd = shape.Y - y;
                creating = false;
            }

            if (creating)
            {
                Shapes.Add(Shape.newDot(x, y, Stype));
            }
        }

        public void RPointerPressed(double x, double y)
        {
            for (var i = 0; i < Shapes.Count; i++)
            {
                if (!Shapes[i].IsInside(x, y)) continue;
                Shapes.Remove(Shapes[i]);
                break;
            }
        }

        public void Pointer_Moved(double X, double Y)
        {
            var update = false;
            foreach (var c in Shapes)
            {
                if (c.Move)
                {
                    c.X = X + c.Xd;
                    c.Y = Y + c.Yd;
                    update = true;
                }
            }

            if (update)
                InvalidateVisual();
        }

        public void PoiterReleased()
        {
            foreach (var c in Shapes)
            {
                c.Move = false;
            }

            InvalidateVisual();
        }

        private void DrawShell(DrawingContext drawingContext, IPen pen, bool Draw)
        {
            foreach (var s in Shapes)
            {
                s.IsShell = false;
            }

            for (var i = 0; i < Shapes.Count - 1; i++)
            {
                var a = Shapes[i];
                for (var j = i + 1; j < Shapes.Count; j++)
                {
                    var b = Shapes[j];

                    var k = (b.Y - a.Y) / (b.X - a.X);
                    var d = a.Y - k * a.X;
                    int above = 0, below = 0, ontheline = 0;

                    for (var l = 0; l < Shapes.Count; l++)
                    {
                        var c = Shapes[l];
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
                if (Shapes.Any(s => s.Move))
                {
                    return;
                }

                for (var i = 0; i < Shapes.Count; i++)
                {
                    var s = Shapes[i];
                    if (!s.IsShell)
                    {
                        Shapes.Remove(s);
                    }
                }
            }
        }

        private void DrawGraham(DrawingContext drawingContext, IPen pen, bool Draw)
        {
            var p0 = Shapes.OrderBy(p => p.Y).ThenBy(p => p.X).First();
            var sortedPoints = Shapes.OrderBy(p => Math.Atan2(p.Y - p0.Y, p.X - p0.X)).ToList();
            var hull = new List<Shape> { sortedPoints[0], sortedPoints[1] };
            foreach (var p in sortedPoints)
            {
                while (hull.Count >= 2)
                {
                    var a = hull.Skip(hull.Count - 2).Take(2).ToList();
                    if (Orientation(a[0], a[1], p) < 0)
                    {
                        hull.RemoveAt(hull.Count - 1);
                    }
                    else
                    {
                        break;
                    }
                }

                hull.Add(p);
            }

            if (Draw)
            {
                bool D = true;
                foreach (var s in Shapes)
                {
                    if (s.Move) D = false;
                }

                if (D)
                {
                    for (var i = 0; i < Shapes.Count; i++)
                    {
                        var s = Shapes[i];
                        if (!hull.Contains(s) && !s.Move)
                        {
                            Shapes.Remove(s);
                            i--;
                        }
                    }
                }

                for (var i = 0; i < hull.Count; i++)
                {
                    drawingContext.DrawLine(pen,
                        new Point(hull[i].X, hull[i].Y),
                        new Point(hull[(i + 1) % hull.Count].X, hull[(i + 1) % hull.Count].Y));
                }
            }
        }

        private void DrawAndrew(DrawingContext drawingContext, IPen pen, bool Draw)
        {
            var sorted_points = Shapes.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
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
                bool D = true;
                foreach (var s in Shapes)
                {
                    if (s.Move) D = false;
                }

                if (D)
                {
                    for (var i = 0; i < Shapes.Count; i++)
                    {
                        var s = Shapes[i];
                        if ((!l.Contains(s) && !u.Contains(s)))
                        {
                            Shapes.Remove(s);
                            i--;
                        }
                    }
                }

                for (var i = 0; i < l.Count - 1; i++)
                {
                    drawingContext.DrawLine(pen,
                        new Point(l[i].X, l[i].Y),
                        new Point(l[i + 1].X, l[(i + 1) % l.Count].Y));
                }

                for (var i = 0; i < u.Count - 1; i++)
                {
                    drawingContext.DrawLine(pen,
                        new Point(u[i].X, u[i].Y),
                        new Point(u[i + 1].X, u[(i + 1) % u.Count].Y));
                }
            }
        }

        private static double Orientation(Shape a, Shape b, Shape c)
        {
            return (b.X - a.X) * (c.Y - b.Y) - (b.Y - a.Y) * (c.X - b.X);
        }

        public override void Render(DrawingContext drawingContext)
        {
            var pen_for_shell = new Pen(Brushes.Purple, 1.7, lineCap: PenLineCap.Round);
            var pen_for_outline = new Pen(Brushes.Black, 1.7, lineCap: PenLineCap.Round);

            var brush = new SolidColorBrush(Colors.GreenYellow);

            if (Shapes.Count > 2)
            {
                DrawAndrew(drawingContext, pen_for_shell, true);
            }

            foreach (var c in Shapes)
            {
                c.Draw(drawingContext, brush, pen_for_outline);
            }
        }

        public void Compare()
        {
            
        }
    }
}