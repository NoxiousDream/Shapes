using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Shapes;

public partial class Settings : Window
{
    public Settings()
    {
        InitializeComponent();
    }

    private void WinCircle(object? sender, PointerPressedEventArgs pointerPressedEventArgs)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC.ChangeToCircle();
    }

    private void WinTriangle(object sender, PointerPressedEventArgs e)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToTriangle();
    }

    private void WinSquare(object sender, PointerPressedEventArgs e)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToSquare();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs routedEventArgs)
    {
        var Graph = new TimeGraphs();
        Graph.ShowDialog(this);
    }

    private void WinDef(object? sender, PointerPressedEventArgs e)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToByDefenition();
    }

    private void WinGra(object? sender, PointerPressedEventArgs e)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToByGraham();
    }

    private void WinAnd(object? sender, PointerPressedEventArgs e)
    {
        var CC = Owner.Find<CustomControl>("myCC");
        CC?.ChangeToByAndrew();
    }

    private void Color_Pick(object? sender, RoutedEventArgs e)
    {
        var CP = new ColourPicker();
        CP.Show(this);
    }
}