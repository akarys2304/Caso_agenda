using agenda_telefonica_back.Models;
using agenda_telefonica_back.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using Npgsql;

namespace agenda_telefonica_back.Repositories
{
    public class ContatoRepositorySql : IContatoRepositorySql
    {
        private readonly string connString = "Host=localhost;Port=5432;Username=postgres;Password=12345678;Database=postgres";

        public async Task<IEnumerable<Contato>> ObterTodosAsync()
        {
            var contatos = new List<Contato>();

            // Script SQL pré-definido
            string sqlScript = @"
                SELECT 
                    id,
                    nome,
                    telefone
                FROM ""Contatos""
                ORDER BY nome ASC";

            try
            {
                using (var connection = new NpgsqlConnection(connString))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        // Define o tipo de comando como Text (script SQL)
                        command.CommandType = CommandType.Text;
                        // Define timeout de 30 segundos
                        command.CommandTimeout = 30;

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var contato = new Contato
                                {
                                    id = reader.GetInt32(0),
                                    nome = reader.GetString(1),
                                    telefone = reader.GetString(2)
                                };

                                contatos.Add(contato);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Erro ao buscar contatos do banco de dados: {ex.Message}", ex);
            }

            return contatos;
        }

        /// <summary>
        /// Obtém um contato específico por ID
        /// Script SQL: SELECT com WHERE
        /// </summary>
        public async Task<Contato> ObterPorIdAsync(int id)
        {
            // Script SQL pré-definido com parâmetro
            string sqlScript = @"
                SELECT 
                    id,
                    nome,
                    telefone
                FROM Contatos
                WHERE Id = @Id";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        command.CommandType = CommandType.Text;

                        // Adiciona parâmetro para prevenir SQL Injection
                        command.Parameters.AddWithValue("@id", id);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Contato
                                {
                                    id = reader.GetInt32(0),
                                    nome = reader.GetString(1),
                                    telefone = reader.GetString(2)
                                };
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Erro ao buscar contato com ID {id}: {ex.Message}", ex);
            }

            return null;
        }

        /// <summary>
        /// Cria um novo contato no banco de dados
        /// Script SQL: INSERT com OUTPUT para retornar o ID gerado
        /// </summary>
        public async Task<Contato> CriarAsync(Contato contato)
        {
            // Script SQL pré-definido com OUTPUT para retornar ID gerado
            string sqlScript = @"
                INSERT INTO Contatos 
                    (nome, telefone)
                VALUES 
                    (@nome, @telefone);
                
                -- Retorna o ID gerado
                SELECT SCOPE_IDENTITY() AS id";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        command.CommandType = CommandType.Text;

                        // Adiciona parâmetros com segurança
                        command.Parameters.AddWithValue("@nome", contato.nome ?? "");
                        command.Parameters.AddWithValue("@telefone", contato.telefone ?? "");

                        // ExecuteScalar retorna o primeiro valor da primeira linha (ID)
                        var result = await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int novoId))
                        {
                            contato.id = novoId;
                            return contato;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Erro ao criar contato no banco de dados: {ex.Message}", ex);
            }

            return null;
        }

        /// <summary>
        /// Atualiza um contato existente
        /// Script SQL: UPDATE com WHERE
        /// </summary>
        public async Task<Contato> AtualizarAsync(Contato contato)
        {
            // Script SQL pré-definido para atualização
            string sqlScript = @"
                UPDATE Contatos
                SET 
                    nome = @nome,
                    telefone = @telefone
                WHERE id = @id";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        command.CommandType = CommandType.Text;

                        // Adiciona parâmetros
                        command.Parameters.AddWithValue("@Id", contato.id);
                        command.Parameters.AddWithValue("@nome", contato.nome ?? "");
                        command.Parameters.AddWithValue("@telefone", contato.telefone ?? "");

                        // ExecuteNonQuery retorna o número de linhas afetadas
                        int linhasAfetadas = await command.ExecuteNonQueryAsync();

                        if (linhasAfetadas > 0)
                        {
                            return contato;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Erro ao atualizar contato com ID {contato.id}: {ex.Message}", ex);
            }

            return null;
        }

        /// <summary>
        /// Deleta um contato do banco de dados
        /// Script SQL: DELETE com WHERE
        /// </summary>
        public async Task<bool> DeletarAsync(int id)
        {
            // Script SQL pré-definido para deleção
            string sqlScript = @"
                DELETE FROM Contatos
                WHERE id = @id";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connString))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlScript, connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@id", id);

                        int linhasAfetadas = await command.ExecuteNonQueryAsync();

                        return linhasAfetadas > 0;
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw new Exception($"Erro ao deletar contato com ID {id}: {ex.Message}", ex);
            }
        }
    }
}
