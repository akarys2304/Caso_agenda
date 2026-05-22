using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using agenda_telefonica_back.Models;

namespace agenda_telefonica_back.Repositories.Interfaces
{
    public interface IContatoRepositorySql
    {
        Task<IEnumerable<Contato>> ObterTodosAsync();
        Task<Contato> ObterPorIdAsync(int id);
        Task<Contato> CriarAsync(Contato contato);
        Task<Contato> AtualizarAsync(Contato contato);
        Task<bool> DeletarAsync(int id);
        Task<IEnumerable<Contato>> ObterPorNomeAsync(string nome);
        Task<int> ObterTotalContatosAsync();
    }
}
