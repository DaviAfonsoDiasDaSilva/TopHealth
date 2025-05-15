using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;
using System.Collections.ObjectModel;
// by sgl
using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Interactivity;
using ScottPlot;
using ScottPlot.Avalonia;

using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.IO;
//

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
            // var lista = await DatabaseMethods.ObterRegistrosDiariosExibicaoAsync();
            // RegistroDiarioGrid.ItemsSource = new ObservableCollection<RegistroDiarioExibicao>(lista);
        
            WriteAll();
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

    private void PlotGrafico(object sender, RoutedEventArgs e){
        var qualidadesSono = new List<string> {"Muito boa","Boa","Mediano","Ruim","Muito Ruim"};

        var myPlot = new ScottPlot.Plot();

        var l_xs = new List<double>{};
        var l_labels = new List<string>();
        var quantities = new List<double>();
        var descricoes = OrdenarSonos(DatabaseMethods.TakeSonos());
        foreach (var ql_sono in descricoes){
            Console.WriteLine(ql_sono);
        }
        int n = 1;
        // Pega a quantidade de cada qualidade do sono
        foreach (var ql_sono1 in qualidadesSono){
            int qt = 0;
            foreach (var ql_sono2 in descricoes){
                if (ql_sono1==ql_sono2){
                    qt++;
                }
            }
            quantities.Add(qt);
        }

        // Coloca em cada 'x'
        foreach (var ql_sono in qualidadesSono){
            l_xs.Add(n);
            n++;
            l_labels.Add(ql_sono);
        }

        // Transforma em array
        double[] xs = l_xs.ToArray();
        double[] ys = quantities.ToArray();
        string[] labels = l_labels.ToArray();

        // Salva imagem
        myPlot.Add.Scatter(xs, ys);
        myPlot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(xs, labels);
        myPlot.SavePng("grafico.png",600,600);
        
        // Puxar imagem
        var bitmap = new Bitmap("./grafico.png");
        grafico.Source = bitmap;
    }
    
    public List<string> OrdenarSonos(List<string> sonosDesordenados){
        var qualidadesSono = new List<string> {"Muito boa","Boa","Mediano","Ruim","Muito Ruim"};
        var sonosOrdenados = new List<string>();
        foreach(var sono1 in qualidadesSono){
            foreach(var sono2 in sonosDesordenados){
                if (sono1 == sono2){
                    sonosOrdenados.Add(sono2);
                }
            }
        }
        return sonosOrdenados;
    }

    private void WriteAll(){ // escrever o regsitro diario num text block
        string str_historico = "";
        int i = 1;

        foreach (var str in DatabaseMethods.TakeAll()){
            str_historico += "_"+str+"_ ";
            i++;
            if (i%5==0){
                str_historico+="\n";
            }
        }

        this.FindControl<TextBlock>("historico").Text = str_historico;
    }
}

public class RegistroDiarioModel
{
    public string Humor { get; set; }
    public string Sono { get; set; }
    public string HabitosAlimentares { get; set; }
    public int AtividadeFisica { get; set; }
}