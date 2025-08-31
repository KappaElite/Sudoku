using Avalonia.Controls;
using Avalonia.Media;

namespace Projekt;

public class InvalidInputState: IState
{
    public void HandleRequest(TextBox textBox)
    {
        textBox.Foreground = Brushes.DarkRed;
        textBox.IsReadOnly = false;
    }
    
    public IState GetState()
    {
        return this;
    }
}
