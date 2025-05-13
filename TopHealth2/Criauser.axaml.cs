using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TopHealth2;

public partial class CriaUser : Window
{
    public CriaUser(){
        InitializeComponent();
    }
    private void NovoRegi(object? sender, RoutedEventArgs e)
    {
        //this.Close(true);
        var NovoRegiWin = new NovoRegistroDiario();
        NovoRegiWin.Show();
        this.Close();
    }
    private void BotRet(object? sender, RoutedEventArgs e)
    {
        var TelaIni = new MainWindow();
        TelaIni.Show();
        this.Close();
    }
}