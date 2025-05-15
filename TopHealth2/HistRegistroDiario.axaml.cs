using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;
using System.Collections.ObjectModel;

namespace TopHealth2;

public partial class HistRegistroDiario : Window
{

    public HistRegistroDiario()
    {
        InitializeComponent();
        CarregarRegistros();

        // Inicialize os dados
        

        // Vincule os dados ao DataGrid usando ItemsSource

    }
    
    private async void CarregarRegistros()
        {
            var lista = await DatabaseMethods.ObterRegistrosDiariosExibicaoAsync();
            RegistroDiarioGrid.ItemsSource = new ObservableCollection<RegistroDiarioExibicao>(lista);
        }

    private async void RegistroDiarioGrid_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
    {
        var registroEditado = e.Row.DataContext as RegistroDiarioModel;
        if (registroEditado != null)
        {
            // Converta RegistroDiarioModel para RegistroDiario
            var registroDiario = ConverterParaRegistroDiario(registroEditado);

            // Salve as alterações no banco de dados
            await DatabaseMethods.AtualizarRegistroDiarioAsync(registroDiario);
            Console.WriteLine("Registro atualizado no banco de dados!");
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
    private RegistroDiario ConverterParaRegistroDiario(RegistroDiarioModel model)
    {
        return new RegistroDiario(
            userId: 1, // Substitua pelo ID do usuário correto
            data: DateTime.Now.ToString("yyyy-MM-dd"), // Ajuste conforme necessário
            humorId: ObterIdHumor(model.Humor),
            sonoId: ObterIdSono(model.Sono),
            alimentacaoId: ObterIdAlimentacao(model.HabitosAlimentares),
            atividadeFisicaId: model.AtividadeFisica,
            id: -1 // ID padrão para novos registros
        );
    }
    private int ObterIdHumor(string humor)
    {
        return humor switch
        {
            "Feliz" => 1,
            "Triste" => 2,
            _ => -1 // Valor padrão para casos desconhecidos
        };
    }

    private int ObterIdSono(string sono)
    {
        return sono switch
        {
            "Muito boa" => 1,
            "Ruim" => 2,
            _ => -1
        };
    }

    private int ObterIdAlimentacao(string habitos)
    {
        return habitos switch
        {
            "Saudável" => 1,
            "Irregular" => 2,
            _ => -1
        };
    }
}

public class RegistroDiarioModel
{
    public string Humor { get; set; }
    public string Sono { get; set; }
    public string HabitosAlimentares { get; set; }
    public int AtividadeFisica { get; set; }
}