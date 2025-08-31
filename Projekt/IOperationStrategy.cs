using Avalonia.Controls;

namespace Projekt;

public interface IOperationStrategy
{
    void InitializeHints();
    int GetNumberOfHints();
    void Handle();
}