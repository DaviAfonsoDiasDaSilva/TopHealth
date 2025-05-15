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
    string descricaoHumor;
    private void HumorSelected(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        if (comboBox.SelectedItem is ComboBoxItem item)
        {
            descricaoHumor = item.Content.ToString();
        }
    }
    string descricaoSono;
    private void SonoQuaSelec(object sender, SelectionChangedEventArgs e)
    {   
        var comboBox = sender as ComboBox;
        if (comboBox.SelectedItem is ComboBoxItem item)
        {
            descricaoSono = item.Content.ToString();
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

    private void BotRet(object? sender, RoutedEventArgs e)
    {
        var TelaIni = new MainWindow();
        TelaIni.Show();
        this.Close();
    }

    private async void BotSalv(object? sender, RoutedEventArgs e)
    {
        /*int UserId = 1;
        string data = DateTime.Now.ToString("yyyy-MM-dd");
        int HumorId = GetIdFromComboBox(Humor);
        int SonoId = GetIdFromComboBox(Sono);
        int AlimentacaoId = GetIdFromTextBox(Alimentacao);
        int AtividadeFisicaId = GetValueFromNumericUpDown(AtividadeFisica);
        
        RegistroDiario regiDia= new RegistroDiario(UserId,data, HumorId, SonoId, AlimentacaoId, AtividadeFisicaId);

        await DatabaseMethods.AdicionarRegistroDiarioAsync(regiDia);*/
         string descricaoAlimentacao = Alimentacao.Text;
         string descricaoAtividade = AtividadeFisicaText.Text;
        int idHumor = await DatabaseMethods.AdicionarHumorAsync(new Humor(-1, descricaoHumor));
        int idSono = await DatabaseMethods.AdicionarQualidadeSonoAsync(new QualidadeSono(-1, descricaoSono));
        int idAlimentacao = await DatabaseMethods.AdicionarAlimentacaoAsync(new Alimentacao(-1, descricaoAlimentacao, 450));
        int idAtividade = await DatabaseMethods.AdicionarAtividadeFisicaAsync(new AtividadeFisica(-1, descricaoAtividade, 30));

        int idRegistro = await DatabaseMethods.AdicionarRegistroDiarioAsync(
            new RegistroDiario(1, "2025-05-14", idHumor, idSono, idAlimentacao, idAtividade)
        );

         // 1. Coletar os valores digitados
    /*string descricaoHumor = DescricaoHumorTextBox.Text;
    string descricaoSono = DescricaoSonoTextBox.Text;
    string descricaoAlimentacao = DescricaoAlimentacaoTextBox.Text;
    int valorEnergetico = (int)ValorEnergeticoUpDown.Value;
    string tipoAtividade = TipoAtividadeTextBox.Text;
    int duracaoMinutos = (int)DuracaoMinutosUpDown.Value;
    string dataRegistro = DataRegistroPicker.SelectedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd");

    // 2. Inserir os dados no banco
    int idHumor = await DatabaseMethods.AdicionarHumorAsync(new Humor(-1, descricaoHumor));
    int idSono = await DatabaseMethods.AdicionarQualidadeSonoAsync(new QualidadeSono(-1, descricaoSono));
    int idAlimentacao = await DatabaseMethods.AdicionarAlimentacaoAsync(new Alimentacao(-1, descricaoAlimentacao, valorEnergetico));
    int idAtividade = await DatabaseMethods.AdicionarAtividadeFisicaAsync(new AtividadeFisica(-1, tipoAtividade, duracaoMinutos));

    // 3. Inserir registro diário (assumindo UserId fixo 1 aqui)
    int idRegistro = await DatabaseMethods.AdicionarRegistroDiarioAsync(
        new RegistroDiario(1, dataRegistro, idHumor, idSono, idAlimentacao, idAtividade)
    );

    // Mensagem de sucesso
    await MessageBox.Show(this, "Registro diário salvo com sucesso!", "Sucesso");*/
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
