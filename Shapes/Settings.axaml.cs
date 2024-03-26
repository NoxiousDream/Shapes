using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Shapes;

public partial class Settings : Window
{
    private Radius_Change rc;
    private bool isInitialized;
    private ColourPicker CP;
    public Settings(Radius_Change rc)
    {
        this.rc = rc;
        InitializeComponent();
        isInitialized = true;
    }
    
    

    private void WinCircle(object? sender, TappedEventArgs tappedEventArgs)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToCircle();
    }

    private void WinTriangle(object? sender, TappedEventArgs tappedEventArgs)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToTriangle();
    }

    private void WinSquare(object? sender, TappedEventArgs tappedEventArgs)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToSquare();
    }

    private void WinDef(object? sender, TappedEventArgs tappedEventArgs)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToByDefenition();
    }

    private void WinGra(object? sender, TappedEventArgs tappedEventArgs)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToByGraham();
    }

    private void WinAnd(object? sender, TappedEventArgs tappedEventArgs)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToByAndrew();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs routedEventArgs)
    {
        var Graph = new TimeGraphs();
        Graph.Show(this);
    }

    private void Color_Pick(object? sender, RoutedEventArgs e)
    {
        if (CP == null || !CP.IsVisible)
        {
            CP = new ColourPicker();
            CP.Show(this);
            
        }
        else
        {
            CP.Activate();
        }
    }

    private void Radius_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        if(isInitialized)
            rc(Convert.ToInt32(e.NewValue));
    }

    private void SaveFile(object? sender, RoutedEventArgs e)
    {
        var dc = new DataClass();
    }
}