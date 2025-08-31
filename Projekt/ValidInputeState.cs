using Avalonia.Controls;
using Avalonia.Media;

namespace Projekt;

public class ValidInputeState: IState
{
    public void HandleRequest(TextBox textBox)
    {
        textBox.Foreground = Brushes.Moccasin;
        textBox.IsReadOnly = true;
    }

    public IState GetState()
    {
        return this;
    }
}