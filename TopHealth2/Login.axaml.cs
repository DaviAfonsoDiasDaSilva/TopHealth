using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TopHealth2;

public partial class Login : Window
{
    public Login(){
        InitializeComponent();
        DataContext = new HistRegistroDiario();
    }
    private async void HistRegi(object? sender, RoutedEventArgs e)
    {
        string nome = Nome.Text?.Trim() ?? "";
        string senha = Senha.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
        {
            Console.WriteLine("Por favor, preencha todos os campos!");
            return;

        }

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
        private void CriaUser(object? sender, RoutedEventArgs e)
    {
        //this.Close(true);
        var criaUserWin = new CriaUser();
        criaUserWin.Show();
        this.Close();
    }
}