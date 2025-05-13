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
    private void BotRet(object? sender, RoutedEventArgs e)
    {
        var TelaIni = new MainWindow();
        TelaIni.Show();
        this.Close();
    }
}