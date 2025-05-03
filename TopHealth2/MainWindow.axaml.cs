using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TopHealth2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void NovoRegi(object? sender, RoutedEventArgs e)
    {
        //this.Close(true);
        var NovoRegiWin = new NovoRegistroDiario();
        NovoRegiWin.Show();
        this.Close();
    }
}