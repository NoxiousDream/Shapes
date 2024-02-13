using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Shapes;

public partial class TimeGraphs : Window
{
    private readonly List<double>[] graphs = new List<double>[3];

    public TimeGraphs()
    {
        //var CC = Owner.Find<CustomControl>("myCC");
        //var CC = Owner
        Console.WriteLine(Owner);
        graphs[0] = new List<double>();
        graphs[1] = new List<double>();
        graphs[2] = new List<double>();
        var rnd = new Random();
        var shapes = new List<Shape>();
        for (var i = 0; i < 50; i++)
        {
            for(int j = 0; j < 10; j++)
                shapes.Add(new Circle(rnd.Next(0, 1000), rnd.Next(0, 1000)));
            //graphs[0].Add(CC.CountTime(Methods.ByDefenition, shapes.ToList()));
            //graphs[1].Add(CC!.CountTime(Methods.ByAndrew, shapes.ToList()));
            //graphs[2].Add(CC!.CountTime(Methods.ByGraham, shapes.ToList()));
        }


        InitializeComponent();
        //graphs[0].RemoveRange(0, 5);
        //graphs[1].RemoveRange(0, 5);
        //graphs[2].RemoveRange(0, 5);
        //this.Find<Graph>("Graphs")!.Draw(0, graphs[0]);
        //this.Find<Graph>("Graphs")!.Draw(1, graphs[1]);
        //this.Find<Graph>("Graphs")!.Draw(2, graphs[2]);
        //this.Find<Graph>("Graphs")!.InvalidateVisual();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        this.Find<Graph>("Graphs")!.Draw(0,
            this.Find<CheckBox>("CBDfn")!.IsChecked == true ? graphs[0] : new List<double>());

        this.Find<Graph>("Graphs")!.Draw(1,
            this.Find<CheckBox>("CBGra")!.IsChecked == true ? graphs[1] : new List<double>());

        this.Find<Graph>("Graphs")!.Draw(1,
            this.Find<CheckBox>("CBAnd")!.IsChecked == true ? graphs[2] : new List<double>());
    }
}