using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Avalonia.Interactivity;

namespace Shapes;

public class DataClass
{
    

    private void LoadState()
    {
        var name = "";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(
            name, FileMode.Open, FileAccess.Read);
        // MyData
    }

    private void SaveFile(object? sender, RoutedEventArgs e)
    {
        
    }
}