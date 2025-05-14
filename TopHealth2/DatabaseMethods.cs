using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace TopHealth2
{
    public static class DatabaseMethods
    {
        // Método para obter uma conexão com o banco
        private static SQLiteConnection ObterConexao()
        {
            var conexao = new SQLiteConnection(Database._caminho);
            conexao.Open();
            return conexao;
        }

        

        public static async Task<int> AdicionarRegistroDiarioAsync(RegistroDiario registro)
        {
            int id = -1;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand(@"
                        INSERT INTO RegistroDiario (UserId, Data, HumorId, SonoId, AlimentacaoId, AtividadeFisicaId) 
                        VALUES (@UserId, @Data, @HumorId, @SonoId, @AlimentacaoId, @AtividadeFisicaId);
                        SELECT last_insert_rowid();", conexao))
                    {
                        comando.Parameters.AddWithValue("@UserId", registro.UserId);
                        comando.Parameters.AddWithValue("@Data", registro.Data);
                        comando.Parameters.AddWithValue("@HumorId", registro.HumorId);
                        comando.Parameters.AddWithValue("@SonoId", registro.SonoId);
                        comando.Parameters.AddWithValue("@AlimentacaoId", registro.AlimentacaoId);
                        comando.Parameters.AddWithValue("@AtividadeFisicaId", registro.AtividadeFisicaId);

                        id = Convert.ToInt32(comando.ExecuteScalar());
                    }

                    Console.WriteLine("Registro criado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao adicionar registro diário: " + ex.Message);
                }
            });
            return id;
        }

        public static async Task<RegistroDiario ?> ObterRegistroDiarioAsync(int id)
        {
            RegistroDiario ? registro = null;  //pode ser null
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM RegistroDiario WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        using (var leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {

                                int idRegistroDiario = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                                int idUsuario = leitor["UserId"] != DBNull.Value ? Convert.ToInt32(leitor["UserId"]) : -1;
                                string data = leitor["Data"] != DBNull.Value ? leitor["Data"].ToString()! : string.Empty;
                                int idHumor = leitor["HumorId"] != DBNull.Value ? Convert.ToInt32(leitor["HumorId"]) : -1;
                                int idSono = leitor["SonoId"] != DBNull.Value ? Convert.ToInt32(leitor["SonoId"]) : -1;
                                int idAlimentacao = leitor["AlimentacaoId"] != DBNull.Value ? Convert.ToInt32(leitor["AlimentacaoId"]) : -1;
                                int idAtividadeFisica = leitor["AtividadeFisicaId"] != DBNull.Value ? Convert.ToInt32(leitor["AtividadeFisicaid"]) : -1;
                                
                                registro = new RegistroDiario( idUsuario, data, idHumor, idSono, idAlimentacao, idAtividadeFisica,idRegistroDiario);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter registro diário: " + ex.Message);
                }
            });
            return registro;
        }

        public static async Task<List<RegistroDiario>> ObterTodosRegistrosDiariosAsync(int userId, string ?dataInicio = null, string ?dataFim = null)
        {
            List<RegistroDiario> registros = new List<RegistroDiario>();
            await Task.Run(() =>
            {
                try
                {
                    string query = "SELECT * FROM RegistroDiario WHERE UserId = @UserId";
                    if (!string.IsNullOrEmpty(dataInicio) && !string.IsNullOrEmpty(dataFim))
                    {
                        query += " AND Data BETWEEN @DataInicio AND @DataFim";
                    }
                    query += " ORDER BY Data DESC";

                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@UserId", userId);
                        if (!string.IsNullOrEmpty(dataInicio) && !string.IsNullOrEmpty(dataFim))
                        {
                            comando.Parameters.AddWithValue("@DataInicio", dataInicio);
                            comando.Parameters.AddWithValue("@DataFim", dataFim);
                        }

                        using (var leitor = comando.ExecuteReader())
                        {
                            while (leitor.Read())
                            {

                                int idRegistroDiario = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                                int idUsuario = leitor["UserId"] != DBNull.Value ? Convert.ToInt32(leitor["UserId"]) : -1;
                                string data = leitor["Data"] != DBNull.Value ? leitor["Data"].ToString()! : string.Empty;
                                int idHumor = leitor["HumorId"] != DBNull.Value ? Convert.ToInt32(leitor["HumorId"]) : -1;
                                int idSono = leitor["SonoId"] != DBNull.Value ? Convert.ToInt32(leitor["SonoId"]) : -1;
                                int idAlimentacao = leitor["AlimentacaoId"] != DBNull.Value ? Convert.ToInt32(leitor["AlimentacaoId"]) : -1;
                                int idAtividadeFisica = leitor["AtividadeFisicaId"] != DBNull.Value ? Convert.ToInt32(leitor["AtividadeFisicaid"]) : -1;

                                registros.Add(new RegistroDiario( idUsuario, data, idHumor, idSono, idAlimentacao, idAtividadeFisica,idRegistroDiario));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter registros diários: " + ex.Message);
                }
            });
            return registros;
        }

        public static async Task<bool> AtualizarRegistroDiarioAsync(RegistroDiario registro)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand(@"
                        UPDATE RegistroDiario 
                        SET UserId = @UserId, 
                            Data = @Data, 
                            HumorId = @HumorId, 
                            SonoId = @SonoId, 
                            AlimentacaoId = @AlimentacaoId, 
                            AtividadeFisicaId = @AtividadeFisicaId 
                        WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@UserId", registro.UserId);
                        comando.Parameters.AddWithValue("@Data", registro.Data);
                        comando.Parameters.AddWithValue("@HumorId", registro.HumorId);
                        comando.Parameters.AddWithValue("@SonoId", registro.SonoId);
                        comando.Parameters.AddWithValue("@AlimentacaoId", registro.AlimentacaoId);
                        comando.Parameters.AddWithValue("@AtividadeFisicaId", registro.AtividadeFisicaId);

                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar registro diário: " + ex.Message);
                }
            });
            return sucesso;
        }

        public static async Task<bool> ExcluirRegistroDiarioAsync(int id)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("DELETE FROM RegistroDiario WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir registro diário: " + ex.Message);
                }
            });
            return sucesso;
        }
      

        
        public static async Task<int> AdicionarHumorAsync(Humor humor)
        {
            int id = -1;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand(@"
                        INSERT INTO Humor (descricao) VALUES (@descricao);
                        SELECT last_insert_rowid();", conexao))
                    {
                        comando.Parameters.AddWithValue("@descricao", humor.Descricao);
                        id = Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao adicionar humor: " + ex.Message);
                }
            });
            return id;
        }

        public static async Task<Humor ?> ObterHumorAsync(int id)
        {
            Humor ? humor = null;  //pode ser null
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM Humor WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        using (var leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                int idHumor = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                                string descricaoHumor = leitor["Descricao"] != DBNull.Value ? leitor["Descricao"].ToString()! : string.Empty;
                                
                                humor = new Humor(idHumor, descricaoHumor);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter humor: " + ex.Message);
                }
            });
            return humor;
        }

        public static async Task<List<Humor>> ObterTodosHumoresAsync()
        {
            List<Humor> humores = new List<Humor>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM Humor", conexao))
                    using (var leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {

                            int idHumor = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                            string descricaoHumor = leitor["Descricao"] != DBNull.Value ? leitor["Descricao"].ToString()! : string.Empty;
                            
                            humores.Add(new Humor(idHumor, descricaoHumor));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter humores: " + ex.Message);
                }
            });
            return humores;
        }

        public static async Task<bool> AtualizarHumorAsync(Humor humor)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("UPDATE Humor SET descricao = @descricao WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", humor.Id);
                        comando.Parameters.AddWithValue("@descricao", humor.Descricao);
                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar humor: " + ex.Message);
                }
            });
            return sucesso;
        }

        public static async Task<bool> ExcluirHumorAsync(int id)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("DELETE FROM Humor WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir humor: " + ex.Message);
                }
            });
            return sucesso;
        }
        

        
        public static async Task<int> AdicionarQualidadeSonoAsync(QualidadeSono sono)
        {
            int id = -1;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand(@"
                        INSERT INTO QualidadeSono (Descricao) VALUES (@Descricao);
                        SELECT last_insert_rowid();", conexao))
                    {
                        comando.Parameters.AddWithValue("@Descricao", sono.Descricao);
                        id = Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao adicionar qualidade de sono: " + ex.Message);
                }
            });
            return id;
        }

        public static async Task<QualidadeSono ? > ObterQualidadeSonoAsync(int id)
        {
            QualidadeSono ? sono = null;  //pode ser null
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM QualidadeSono WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        using (var leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {

                                int idQualidadeSono = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                                string descricaoQualidadeSono = leitor["Descricao"] != DBNull.Value ? leitor["Descricao"].ToString()! : string.Empty;
                                
                                sono = new QualidadeSono(idQualidadeSono, descricaoQualidadeSono);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter qualidade de sono: " + ex.Message);
                }
            });
            return sono;
        }

        public static async Task<List<QualidadeSono>> ObterTodasQualidadesSonoAsync()
        {
            List<QualidadeSono> qualidades = new List<QualidadeSono>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM QualidadeSono", conexao))
                    using (var leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {

                            int idQualidadeSono = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                            string descricaoQualidadeSono = leitor["Descricao"] != DBNull.Value ? leitor["Descricao"].ToString()! : string.Empty;
                            
                            qualidades.Add(new QualidadeSono(idQualidadeSono, descricaoQualidadeSono));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter qualidades de sono: " + ex.Message);
                }
            });
            return qualidades;
        }

        public static async Task<bool> AtualizarQualidadeSonoAsync(QualidadeSono sono)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("UPDATE QualidadeSono SET Descricao = @Descricao WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", sono.Id);
                        comando.Parameters.AddWithValue("@Descricao", sono.Descricao);
                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar qualidade de sono: " + ex.Message);
                }
            });
            return sucesso;
        }

        public static async Task<bool> ExcluirQualidadeSonoAsync(int id)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("DELETE FROM QualidadeSono WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir qualidade de sono: " + ex.Message);
                }
            });
            return sucesso;
        }
       
        public static async Task<int> AdicionarAlimentacaoAsync(Alimentacao alimentacao)
        {
            int id = -1;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand(@"
                        INSERT INTO Alimentacao (Descricao, ValorEnergetico) VALUES (@Descricao, @ValorEnergetico);
                        SELECT last_insert_rowid();", conexao))
                    {
                        comando.Parameters.AddWithValue("@Descricao", alimentacao.Descricao);
                        comando.Parameters.AddWithValue("@ValorEnergetico", alimentacao.ValorEnergetico);
                        id = Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao adicionar alimentação: " + ex.Message);
                }
            });
            return id;
        }

        public static async Task<Alimentacao ?> ObterAlimentacaoAsync(int id)
        {
            Alimentacao ? alimentacao = null;  //pode ser null
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM Alimentacao WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        using (var leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {

                                int idAlimentacao = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                                string descricao = leitor["Descricao"] != DBNull.Value? leitor["Descricao"].ToString()! : string.Empty;
                                int valorEnergetico1 = leitor["ValorEnergetico"] != DBNull.Value ? Convert.ToInt32(leitor["ValorEnergetico"]) : 0;
                                
                                alimentacao = new Alimentacao(idAlimentacao, descricao, valorEnergetico1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter alimentação: " + ex.Message);
                }
            });
            return alimentacao;
        }

        public static async Task<List<Alimentacao>> ObterTodasAlimentacoesAsync()
        {
            List<Alimentacao> alimentacoes = new List<Alimentacao>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM Alimentacao", conexao))
                    using (var leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {

                            int idAlimentacao = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                            string descricao = leitor["Descricao"] != DBNull.Value? leitor["Descricao"].ToString()! : string.Empty;
                            int valorEnergetico1 = leitor["ValorEnergetico"] != DBNull.Value ? Convert.ToInt32(leitor["ValorEnergetico"]) : 0;
                            
                            alimentacoes.Add(new Alimentacao(idAlimentacao, descricao, valorEnergetico1));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter alimentações: " + ex.Message);
                }
            });
            return alimentacoes;
        }

        public static async Task<bool> AtualizarAlimentacaoAsync(Alimentacao alimentacao)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand(@"
                        UPDATE Alimentacao 
                        SET Descricao = @Descricao, ValorEnergetico = @ValorEnergetico 
                        WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", alimentacao.Id);
                        comando.Parameters.AddWithValue("@Descricao", alimentacao.Descricao);
                        comando.Parameters.AddWithValue("@ValorEnergetico", alimentacao.ValorEnergetico);
                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar alimentação: " + ex.Message);
                }
            });
            return sucesso;
        }

        public static async Task<bool> ExcluirAlimentacaoAsync(int id)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("DELETE FROM Alimentacao WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir alimentação: " + ex.Message);
                }
            });
            return sucesso;
        }


        public static async Task<int> AdicionarAtividadeFisicaAsync(AtividadeFisica atividade)
        {
            int id = -1;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand(@"
                        INSERT INTO AtividadeFisica (TipoAtividade, DuracaoMinutos) VALUES (@TipoAtividade, @DuracaoMinutos);
                        SELECT last_insert_rowid();", conexao))
                    {
                        comando.Parameters.AddWithValue("@TipoAtividade", atividade.TipoAtividade);
                        comando.Parameters.AddWithValue("@DuracaoMinutos", atividade.DuracaoMinutos);
                        id = Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao adicionar atividade física: " + ex.Message);
                }
            });
            return id;
        }

        public static async Task<AtividadeFisica ?> ObterAtividadeFisicaAsync(int id)
        {
            AtividadeFisica ? atividade = null; //pode ser null
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM AtividadeFisica WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        using (var leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                int idAtividade = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                                string tipo = leitor["TipoAtividade"] != DBNull.Value ? leitor["TipoAtividade"].ToString()! : string.Empty;
                                int duracao = leitor["DuracaoMinutos"] != DBNull.Value ? Convert.ToInt32(leitor["DuracaoMinutos"]) : 0;

                                atividade = new AtividadeFisica(idAtividade, tipo, duracao);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter atividade física: " + ex.Message);
                }
            });
            return atividade;
        }


        public static async Task<List<AtividadeFisica>> ObterTodasAtividadesFisicasAsync()
        {
            List<AtividadeFisica> atividades = new List<AtividadeFisica>();
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM AtividadeFisica", conexao))
                    using (var leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {

                            int idAtividade = leitor["id"] != DBNull.Value ? Convert.ToInt32(leitor["id"]) : -1;
                            string tipo = leitor["TipoAtividade"] != DBNull.Value ? leitor["TipoAtividade"].ToString()! : string.Empty;
                            int duracao = leitor["DuracaoMinutos"] != DBNull.Value ? Convert.ToInt32(leitor["DuracaoMinutos"]) : 0;

                            atividades.Add(new AtividadeFisica(idAtividade, tipo, duracao));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter atividades físicas: " + ex.Message);
                }
            });
            return atividades;
        }

        public static async Task<bool> AtualizarAtividadeFisicaAsync(AtividadeFisica atividade)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand(@"
                        UPDATE AtividadeFisica 
                        SET TipoAtividade = @TipoAtividade, DuracaoMinutos = @DuracaoMinutos 
                        WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", atividade.Id);
                        comando.Parameters.AddWithValue("@TipoAtividade", atividade.TipoAtividade);
                        comando.Parameters.AddWithValue("@DuracaoMinutos", atividade.DuracaoMinutos);
                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao atualizar atividade física: " + ex.Message);
                }
            });
            return sucesso;
        }

        public static async Task<bool> ExcluirAtividadeFisicaAsync(int id)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("DELETE FROM AtividadeFisica WHERE id = @id", conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        sucesso = comando.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao excluir atividade física: " + ex.Message);
                }
            });
            return sucesso;
        }

        
        public static async Task<Configuracao ? > ObterConfiguracaoAsync(int userId)
        {
            Configuracao ? configuracao = null;  //pode ser null
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    using (var comando = new SQLiteCommand("SELECT * FROM Configuracao WHERE UserId = @UserId", conexao))
                    {
                        comando.Parameters.AddWithValue("@UserId", userId);
                        using (var leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {

                                int idUsuario = leitor["UserId"] != DBNull.Value ? Convert.ToInt32(leitor["UserId"]) : -1;
                                string caminhoBanco = leitor["CaminhoBanco"] != DBNull.Value ? leitor["CaminhoBanco"].ToString()! : string.Empty;
                                string tema = leitor["Tema"] != DBNull.Value ? leitor["Tema"].ToString()! : string.Empty;
                                int FlagImportacao1 = leitor["FlagImportacao"] != DBNull.Value ? Convert.ToInt32(leitor["FlagImportacao"]) : 0;

                                configuracao = new Configuracao(idUsuario, caminhoBanco, tema, FlagImportacao1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter configuração: " + ex.Message);
                }
            });
            return configuracao;
        }

        public static async Task<bool> CriarOuAtualizarConfiguracaoAsync(Configuracao configuracao)
        {
            bool sucesso = false;
            await Task.Run(() =>
            {
                try
                {
                    using (var conexao = ObterConexao())
                    {
    
                        using (var comando = new SQLiteCommand("SELECT COUNT(*) FROM Configuracao WHERE UserId = @UserId", conexao))
                        {
                            comando.Parameters.AddWithValue("@UserId", configuracao.UserId);
                            int count = Convert.ToInt32(comando.ExecuteScalar());

                            if (count > 0)
                            {
                                // Atualiza a configuração existente
                                using (var comandoUpdate = new SQLiteCommand(@"
                                    UPDATE Configuracao 
                                    SET CaminhoBanco = @CaminhoBanco, 
                                        Tema = @Tema, 
                                        FlagImportacao = @FlagImportacao 
                                    WHERE UserId = @UserId", conexao))
                                {
                                    comandoUpdate.Parameters.AddWithValue("@UserId", configuracao.UserId);
                                    comandoUpdate.Parameters.AddWithValue("@CaminhoBanco", configuracao.CaminhoBanco);
                                    comandoUpdate.Parameters.AddWithValue("@Tema", configuracao.Tema);
                                    comandoUpdate.Parameters.AddWithValue("@FlagImportacao", configuracao.FlagImportacao);
                                    sucesso = comandoUpdate.ExecuteNonQuery() > 0;
                                }
                            }
                            else
                            {
                                // Cria uma nova configuração
                                using (var comandoInsert = new SQLiteCommand(@"
                                    INSERT INTO Configuracao (UserId, CaminhoBanco, Tema, FlagImportacao) 
                                    VALUES (@UserId, @CaminhoBanco, @Tema, @FlagImportacao)", conexao))
                                {
                                    comandoInsert.Parameters.AddWithValue("@UserId", configuracao.UserId);
                                    comandoInsert.Parameters.AddWithValue("@CaminhoBanco", configuracao.CaminhoBanco);
                                    comandoInsert.Parameters.AddWithValue("@Tema", configuracao.Tema);
                                    comandoInsert.Parameters.AddWithValue("@FlagImportacao", configuracao.FlagImportacao);
                                    sucesso = comandoInsert.ExecuteNonQuery() > 0;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Tratar exceções caso ocorram
                    sucesso = false;
                    Console.WriteLine("Erro: " + ex.Message);
                }
            });
            return sucesso;
        }

    }
}
