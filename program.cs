
using Avalonia;
using System;
using System.Data.SQLite;

namespace TopHealth;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        Database.TestarConexao();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}

public static class Database
{
    private static string _caminho = @"Data Source=D:\Desktop\bancoTAPOO.db";

    public static void TestarConexao()
    {
        try
        {
            using (var conexao = new SQLiteConnection(_caminho))
            {
                conexao.Open();

                // Obter as tabelas no banco
                using (var comando = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table';", conexao))
                using (var leitor = comando.ExecuteReader())
                {
                    Console.WriteLine("Tabelas no banco:");
                    while (leitor.Read())
                    {
                        string nomeTabela = leitor.GetString(0);
                        Console.WriteLine($"Tabela: {nomeTabela}");
                        
                        // Obter as colunas da tabela
                        using (var comandoColunas = new SQLiteCommand($"PRAGMA table_info({nomeTabela});", conexao))
                        using (var leitorColunas = comandoColunas.ExecuteReader())
                        {
                            Console.WriteLine("Colunas:");
                            while (leitorColunas.Read())
                            {
                                Console.WriteLine($"- {leitorColunas["name"]} (Tipo: {leitorColunas["type"]})");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao conectar com o banco: " + ex.Message);
        }
    }

}

public class RegistroDiário
{
    int id;
    int UserId;
    string  Data;
    int HumorId;
    int SonoId;
    int AlimentacaoId;
    int AtividadeFisicaId;

    public RegistroDiário(int id, int UserId, string Data, int HumorId, int SonoId, int AlimentacaoId, int AtividadeFisicaId)
    {
        this.id = id;
        this.UserId = UserId;
        this.Data = Data;
        this.HumorId = HumorId;
        this.SonoId = SonoId;
        this.AlimentacaoId = AlimentacaoId;
        this.AtividadeFisicaId = AtividadeFisicaId;
    }

}
public class Humor
{
    int id;
    string descricao;
    public Humor(int id, string descricao)
    {
        this.id = id;
        this.descricao = descricao;
    }
}

public class QualidadeSono
{
    int id;
    string Descricao;

    public QualidadeSono(int id, string Descricao)
    {
        this.id = id;
        this.Descricao = Descricao;
    }
}

public class Alimentacao
{
    int id;
    string Descricao;
    int ValorEnergetico;

    public Alimentacao(int id, string Descricao, int ValorEnergetico)
    {
        this.id = id;
        this.Descricao = Descricao;
        this.ValorEnergetico = ValorEnergetico;
    }
}

public class AtividadeFisica
{
    int id;
    string TipoAtividade;
    int DuracaoMinutos;

    public AtividadeFisica(int id, string TipoAtividade, int DuracaoMinutos)
    {
        this.id = id;
        this.TipoAtividade = TipoAtividade;
        this.DuracaoMinutos = DuracaoMinutos;
    }
}

public class Configuracao
{
    int UserId;
    string CaminhoBanco;
    string Tema;
    int FlagImportacao;

    public Configuracao(int UserId, string CaminhoBanco, string Tema, int FlagImportacao)
    {
        this.UserId = UserId;
        this.CaminhoBanco = CaminhoBanco;
        this.Tema = Tema;
        this.FlagImportacao = FlagImportacao;
    }
}

