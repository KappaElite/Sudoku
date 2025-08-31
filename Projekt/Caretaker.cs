using System.Collections.Generic;
using System.Linq;

namespace Projekt;

public class Caretaker
{
    private List<Memento> _mementos = new List<Memento>();


    public void AddMemento(Memento memento)
    {
        if (memento.GetNumberOfPossibleBlankSpaces() > Memento.GetMinNumberOfBlankSpaces())
        {
            _mementos.Add(memento);
            Memento.SetMinNumberOfBlankSpaces(memento.GetNumberOfPossibleBlankSpaces());
            SortMementos();
        }
        
    }

    private void SortMementos()
    {
        _mementos = _mementos.OrderBy(m => m.GetNumberOfPossibleBlankSpaces()).ToList();
    }

    public Memento GetMemento(int index)
    {
        return _mementos[index];
    }

    public Memento GetLast()
    {
        return _mementos.Last();
    }
}