using Avalonia;
using System;
using System.Data.SQLite;

namespace TopHealth2;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}

public static class Database
{
   public static string _caminho = @"Data Source=D:\Desktop\bancoTAPOO.db";

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
    private int _id;
    private int _userId;
    private string _data;
    private int _humorId;
    private int _sonoId;
    private int _alimentacaoId;
    private int _atividadeFisicaId;

    public int Id 
    { 
        get { return _id; } 
        set { _id = value; } 
    }

    public int UserId 
    { 
        get { return _userId; } 
        set { _userId = value; } 
    }

    public string Data 
    { 
        get { return _data; } 
        set { _data = value; } 
    }

    public int HumorId 
    { 
        get { return _humorId; } 
        set { _humorId = value; } 
    }

    public int SonoId 
    { 
        get { return _sonoId; } 
        set { _sonoId = value; } 
    }

    public int AlimentacaoId 
    { 
        get { return _alimentacaoId; } 
        set { _alimentacaoId = value; } 
    }

    public int AtividadeFisicaId 
    { 
        get { return _atividadeFisicaId; } 
        set { _atividadeFisicaId = value; } 
    }

    public RegistroDiário(int id, int userId, string data, int humorId, int sonoId, int alimentacaoId, int atividadeFisicaId)
    {
        _id = id;
        _userId = userId;
        _data = data;
        _humorId = humorId;
        _sonoId = sonoId;
        _alimentacaoId = alimentacaoId;
        _atividadeFisicaId = atividadeFisicaId;
    }
}

public class Humor
{
    private int _id;
    private string _descricao;

    public int Id 
    { 
        get { return _id; } 
        set { _id = value; } 
    }

    public string Descricao 
    { 
        get { return _descricao; } 
        set { _descricao = value; } 
    }

    public Humor(int id, string descricao)
    {
        _id = id;
        _descricao = descricao;
    }
}

public class QualidadeSono
{
    private int _id;
    private string _descricao;

    public int Id 
    { 
        get { return _id; } 
        set { _id = value; } 
    }

    public string Descricao 
    { 
        get { return _descricao; } 
        set { _descricao = value; } 
    }

    public QualidadeSono(int id, string descricao)
    {
        _id = id;
        _descricao = descricao;
    }
}

public class Alimentacao
{
    private int _id;
    private string _descricao;
    private int _valorEnergetico;

    public int Id 
    { 
        get { return _id; } 
        set { _id = value; } 
    }

    public string Descricao 
    { 
        get { return _descricao; } 
        set { _descricao = value; } 
    }

    public int ValorEnergetico 
    { 
        get { return _valorEnergetico; } 
        set { _valorEnergetico = value; } 
    }

    public Alimentacao(int id, string descricao, int valorEnergetico)
    {
        _id = id;
        _descricao = descricao;
        _valorEnergetico = valorEnergetico;
    }
}

public class AtividadeFisica
{
    private int _id;
    private string _tipoAtividade;
    private int _duracaoMinutos;

    public int Id 
    { 
        get { return _id; } 
        set { _id = value; } 
    }

    public string TipoAtividade 
    { 
        get { return _tipoAtividade; } 
        set { _tipoAtividade = value; } 
    }

    public int DuracaoMinutos 
    { 
        get { return _duracaoMinutos; } 
        set { _duracaoMinutos = value; } 
    }

    public AtividadeFisica(int id, string tipoAtividade, int duracaoMinutos)
    {
        _id = id;
        _tipoAtividade = tipoAtividade;
        _duracaoMinutos = duracaoMinutos;
    }
}

public class Configuracao
{
    private int _userId;
    private string _caminhoBanco;
    private string _tema;
    private int _flagImportacao;

    public int UserId 
    { 
        get { return _userId; } 
        set { _userId = value; } 
    }

    public string CaminhoBanco 
    { 
        get { return _caminhoBanco; } 
        set { _caminhoBanco = value; } 
    }

    public string Tema 
    { 
        get { return _tema; } 
        set { _tema = value; } 
    }

    public int FlagImportacao 
    { 
        get { return _flagImportacao; } 
        set { _flagImportacao = value; } 
    }

    public Configuracao(int userId, string caminhoBanco, string tema, int flagImportacao)
    {
        _userId = userId;
        _caminhoBanco = caminhoBanco;
        _tema = tema;
        _flagImportacao = flagImportacao;
    }
}