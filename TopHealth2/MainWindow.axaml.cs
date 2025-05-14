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

    private void Login(object? sender, RoutedEventArgs e){
        var LoginWin = new Login();
        LoginWin.Show();
        this.Close();
    }
    
}