using Avalonia.Controls;

namespace Projekt;

public class SingleSudokuField:IObserver
{
    private TextBox _field;
    private IState _currentState;
    
    public SingleSudokuField(TextBox textBox)
    {
        _field = textBox;
        if (textBox.Text == "")
        {
            SetState(new InvalidInputState());
            
        }
        else
        {
            SetState(new ValidInputeState());
           
        }
        Request();
    }
    
    public void Update(TextBox textBox)
    {
        int result;
        if (int.TryParse(textBox.Text, out result))
        {
            _field = textBox;
        }
        
    }

    public void SetState(IState state)
    {
        _currentState = state;
        Request();
    }

    public void Request()
    {
        _currentState.HandleRequest(_field);
    }

    public IState GetState()
    {
        return _currentState.GetState();
    }

    public TextBox GetTextBox()
    {
        return _field;
    }

    public int ReturnConvertedNumber()
    {
        int tmp;
        if (int.TryParse(_field.Text, out tmp))
        {
            return tmp;
        }

        return -1;
    }
    
    
    
}