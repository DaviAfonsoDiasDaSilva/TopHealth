using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

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

}