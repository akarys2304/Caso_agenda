using agenda_telefonica_back.Models;

namespace agenda_telefonica_back.Repositories.Interfaces
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> ObterTodosAsync();
        Task<Contato> ObterPorIdAsync(int id);
        Task<Contato> CriarAsync(Contato contato);
        Task<Contato> AtualizarAsync(Contato contato);
        Task<bool> DeletarAsync(int id);
        Task<bool> SalvarAsync();
    }
}
