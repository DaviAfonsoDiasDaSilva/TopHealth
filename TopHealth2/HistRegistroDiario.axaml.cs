using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;

namespace TopHealth2;

public partial class HistRegistroDiario : Window
{
    public HistRegistroDiario()
    {
        InitializeComponent();
    }
        private void HumorHist(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        if (comboBox.SelectedItem is ComboBoxItem item)
        {
            
        }
    }


    private void SonoQuaHist(object sender, SelectionChangedEventArgs e)
    {   
        var comboBox = sender as ComboBox;
        if (comboBox.SelectedItem is ComboBoxItem item)
        {
            
        }
    }

    private void BotRetHist(object? sender, RoutedEventArgs e)
    {
        var TelaIni = new MainWindow();
        TelaIni.Show();
        this.Close();
    }
    private void BotSalvEdit(object? sender, RoutedEventArgs e)
    {

    }

    
}