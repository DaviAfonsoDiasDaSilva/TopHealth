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

    /* private void BotSalv(object? sender, RoutedEventArgs e)
    {
        //double valor = QuantAtvFisc.Value ?? 0;
        //MessageBox.Show(this, $"Valor atual: {valor}", "Info");
    }*/
}