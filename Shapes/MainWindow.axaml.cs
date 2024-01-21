using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Shapes;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Win_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var cc = this.Find<CustomControl>("myCC");
        if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.RightButtonPressed)
        {
            cc?.RPointerPressed(e.GetPosition(cc).X, e.GetPosition(cc).Y);
        }
        else if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
        {
            cc?.LPointerPressed(e.GetPosition(cc).X, e.GetPosition(cc).Y);
        }
    }

    private void Win_PointerMoved(object? sender, PointerEventArgs e)
    {
        var cc = this.Find<CustomControl>("myCC");
        cc?.Pointer_Moved(e.GetPosition(cc).X, e.GetPosition(cc).Y);
    }

    private void Win_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        var cc = this.Find<CustomControl>("myCC");
        cc?.PoiterReleased();
    }

    private void WinCircle(object sender, PointerPressedEventArgs e)
    {
        CustomControl? CC = this.Find<CustomControl>("myCC");
        CC?.ChangeToCircle();
    }

    private void WinTriangle(object sender, PointerPressedEventArgs e)
    {
        CustomControl? CC = this.Find<CustomControl>("myCC");
        CC?.ChangeToTriangle();
    }

    private void WinSquare(object sender, PointerPressedEventArgs e)
    {
        CustomControl? CC = this.Find<CustomControl>("myCC");
        CC?.ChangeToSquare();
    }

    private void WinDef(object? sender, PointerPressedEventArgs e)
    {
        CustomControl? CC = this.Find<CustomControl>("myCC");
        CC?.ChangeToByDefenition();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var Graph = new Comparer();
        Graph.ShowDialog(this);
    }

    private void WinGra(object? sender, PointerPressedEventArgs e)
    {
        CustomControl? CC = this.Find<CustomControl>("myCC");
        CC?.ChangeToByGraham();
    }

    private void WinAnd(object? sender, PointerPressedEventArgs e)
    {
        CustomControl? CC = this.Find<CustomControl>("myCC");
        CC?.ChangeToByAndrew();
    }

    private void WinForma(object? sender, PointerPressedEventArgs e)
    {
        
    }

    private void Color_Pick(object? sender, RoutedEventArgs e)
    {
        
    }
}