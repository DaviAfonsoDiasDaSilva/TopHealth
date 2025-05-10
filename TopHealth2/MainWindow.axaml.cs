using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TopHealth2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
         DataContext = new NovoRegistroDiario();
         DataContext = new HistRegistroDiario();
    }
    private void NovoRegi(object? sender, RoutedEventArgs e)
    {
        //this.Close(true);
        var NovoRegiWin = new NovoRegistroDiario();
        NovoRegiWin.Show();
        this.Close();
    }

    private void  HistRegi(object? sender, RoutedEventArgs e){
        var HistRegiWin = new HistRegistroDiario();
        HistRegiWin.Show();
        this.Close();
    }
}