using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TopHealth2;

public partial class Login : Window
{
    public Login(){
        InitializeComponent();
        DataContext = new HistRegistroDiario();
    }
    private void  HistRegi(object? sender, RoutedEventArgs e){
        var HistRegiWin = new HistRegistroDiario();
        HistRegiWin.Show();
        this.Close();
    }
}