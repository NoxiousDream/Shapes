using Avalonia.Controls;

namespace Shapes;

public partial class ColourPicker : Window
{
    public ColourPicker()
    {
        InitializeComponent();
    }

    private void ColorView_OnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        var a = e.NewColor;
        var CC = Owner.Owner.Find<CustomControl>("myCC");
        CC?.ChangeColor(a);
    }
}