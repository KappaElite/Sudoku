using Avalonia.Controls;

namespace Projekt;

public class HardModeStrategy: IOperationStrategy
{
    private int _numberOfHints;
    private TextBlock _textBlock;

    public HardModeStrategy(TextBlock textBlock)
    {
        _textBlock = textBlock;
        _numberOfHints = 2;
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