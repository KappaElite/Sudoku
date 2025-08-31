using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Projekt;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        var newWindow = Main.GetInstance();
        Hide();
        newWindow.Show();
    }
}