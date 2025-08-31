using Avalonia.Controls;

namespace Projekt;

public interface IState
{
    void HandleRequest(TextBox textBox);
    public IState GetState();
}