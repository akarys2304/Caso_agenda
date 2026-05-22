using agenda_telefonica_back.Models.DTOs;

namespace agenda_telefonica_back.Services.Interfaces
{
    public interface IContatoService
    {
        Task<IEnumerable<ContatoDTO>> ObterTodosAsync();
        Task<ContatoDTO> ObterPorIdAsync(int id);
        Task<ContatoDTO> CriarAsync(CriarContatoDTO criarContatoDTO);
        Task<ContatoDTO> AtualizarAsync(int id, AtualizarContatoDTO atualizarContatoDTO);
        Task<bool> DeletarAsync(int id);
    }
}
