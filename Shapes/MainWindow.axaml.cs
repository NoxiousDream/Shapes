using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Shapes;

public partial class MainWindow : Window
{
    private Settings settings;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Win_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var cc = this.Find<CustomControl>("myCC");
        if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.RightButtonPressed)
            cc?.RPointerPressed(e.GetPosition(cc).X, e.GetPosition(cc).Y);
        else if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
            cc?.LPointerPressed(e.GetPosition(cc).X, e.GetPosition(cc).Y);
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

    private void Open_Settings(object? sender, RoutedEventArgs e)
    {
        if (settings == null || !settings.IsVisible)
        {
            settings = new Settings(myCC.ChangeRadius);
            settings.Show(this);
            
        }
        else
        {
            settings.Activate();
        }
    }
}