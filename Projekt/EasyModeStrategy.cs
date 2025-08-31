using Avalonia.Controls;

namespace Projekt;

public class EasyModeStrategy: IOperationStrategy
{
    private int _numberOfHints;
    private TextBlock _textBlock;
    public EasyModeStrategy(TextBlock textBlock)
    {
        _numberOfHints = 5;
        _textBlock = textBlock;
    }
    public void InitializeHints()
    {
        _textBlock.Text = $"Hints Available: {_numberOfHints}";
    }
    
    public int GetNumberOfHints()
    {
        return _numberOfHints;
    }

    public void Handle()
    {
        _numberOfHints--;
        InitializeHints();
    }
}