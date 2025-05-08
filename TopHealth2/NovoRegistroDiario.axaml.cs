using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;

namespace TopHealth2;

public partial class NovoRegistroDiario : Window
{
    public NovoRegistroDiario()
    {
        InitializeComponent();
    }
    private void HumorSelected(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        if (comboBox.SelectedItem is ComboBoxItem item)
        {
            
        }
    }

    private void SonoQuaSelec(object sender, SelectionChangedEventArgs e)
    {   
        var comboBox = sender as ComboBox;
        if (comboBox.SelectedItem is ComboBoxItem item)
        {
            
        }
    }

    private void BotRet(object? sender, RoutedEventArgs e)
    {
        var TelaIni = new MainWindow();
        TelaIni.Show();
        this.Close();
    }

    private async void BotSalv(object? sender, RoutedEventArgs e)
    {
        int novoId = -1;
        int novoUseId = -1;
        string data = DateTime.Now.ToString("yyyy-MM-dd");
        int HumorId = -1;
        int SonoId = -1;
        int AlimentacaoId = -1;
        int AtividadeFisicaId = -1;
        
        RegistroDiario regiDia= new RegistroDiario(novoId,novoUseId,data, HumorId, SonoId, AlimentacaoId, AtividadeFisicaId);

        await DatabaseMethods.AdicionarRegistroDiarioAsync(regiDia);
    }
}