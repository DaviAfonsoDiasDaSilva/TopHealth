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

    /*private void (object sender, SelectionChangedEventArgs e)
    {   
        var textBox = sender as TextBox;
    if (textBox != null)
    {
        // Faça algo com textBox.Text
    }
    }*/

    private void AtvFisQuaSelec(object sender, SelectionChangedEventArgs e)
    {   
 
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
        int HumorId = GetIdFromComboBox(Humor);
        int SonoId = GetIdFromComboBox(Sono);
        int AlimentacaoId = GetIdFromTextBox(Alimentacao);
        int AtividadeFisicaId = GetValueFromNumericUpDown(AtividadeFisica);
        
        RegistroDiario regiDia= new RegistroDiario(novoId,novoUseId,data, HumorId, SonoId, AlimentacaoId, AtividadeFisicaId);

        await DatabaseMethods.AdicionarRegistroDiarioAsync(regiDia);
    }

    private int GetIdFromComboBox(ComboBox comboBox)
    {
        if (comboBox.SelectedItem is ComboBoxItem item && item.Tag is string tagStr && int.TryParse(tagStr, out int id))
        {
            return id;
        }
        return -1; // valor padrão caso nada seja selecionado
    }
    private int GetIdFromTextBox(TextBox textBox)
    {
        if (int.TryParse(textBox.Text, out int id))
        {
            return id;
        }
        return -1; // valor padrão caso a conversão falhe
    }
    private int GetValueFromNumericUpDown(NumericUpDown numericUpDown)
    {
        return (int)numericUpDown.Value;
    }
}
