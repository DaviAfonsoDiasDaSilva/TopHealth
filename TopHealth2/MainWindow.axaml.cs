using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TopHealth2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
         DataContext = new NovoRegistroDiario();
         
    }
    private void CriaUser(object? sender, RoutedEventArgs e)
    {
        //this.Close(true);
        var criaUserWin = new CriaUser();
        criaUserWin.Show();
        this.Close();
    }
    private void Login(object? sender, RoutedEventArgs e){
        var LoginWin = new Login();
        LoginWin.Show();
        this.Close();
    }
    
}