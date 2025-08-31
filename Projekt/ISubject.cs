using Avalonia.Controls;

namespace Projekt;

public interface ISubject
{
    void AddObserver(IObserver observer, int x, int y);
    void NotifyObserver(IObserver observer, TextBox textBox);
    
    //RemoveObserver jest nipepotrzebny w moim kodzie, więc nie implementowalem go niepotrzbnie 
    //void RemoveObserver(IObserver observer);
    
}