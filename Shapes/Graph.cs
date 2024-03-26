using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Shapes;

public class Graph : UserControl
{
    private readonly Brush brush = new SolidColorBrush(Colors.Black);
    private readonly List<double>[] graphs = new List<double>[3];
    private readonly Pen[] pens = { new(Brushes.Blue), new(Brushes.Yellow), new(Brushes.Crimson)};

    public override void Render(DrawingContext drawingContext)
    {
        double x = 10;
        var i = 0;
        var maxElement = double.Max(Max(graphs[0]), double.Max( Max(graphs[1]), Max(graphs[2])));
        foreach (var graph in graphs)
            if (graph != null)
            {
                var step = graph.Count / ((int)Width - 10);
                double xstep = 1;
                if (step == 0)
                {
                    step = 1;
                    xstep = Width / graph.Count;
                }

                var graphPoints = new List<Point>();
                for (var j = 0; j < graph.Count; j += step)
                {
                    graphPoints.Add(new Point(x, (1 - graph[j] / (float)maxElement) * Height));
                    x += xstep;
                }

                drawingContext.DrawGeometry(brush, pens[i], new PolylineGeometry(graphPoints, false));
                i++;
                x = 10;
            }
    }

    private double Max(List<double> Nums)
    {
        if (Nums == null) return long.MinValue;
        double max = 0;
        foreach (var item in Nums)
            if (item > max)
                max = item;
        return max;
    }

    public void Draw(int graphInd, List<double> graphPoints)
    {
        graphs[graphInd] = graphPoints;
        InvalidateVisual();
    }
}