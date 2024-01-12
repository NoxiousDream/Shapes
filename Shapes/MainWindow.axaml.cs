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

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var Window = this.Find<Comparer>("Comparer_Name");
        Console.WriteLine(Window);
        Window.InitializeComponent();
    }
}