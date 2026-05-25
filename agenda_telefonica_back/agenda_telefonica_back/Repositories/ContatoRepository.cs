using Microsoft.EntityFrameworkCore;
using agenda_telefonica_back.Data;
using agenda_telefonica_back.Models;
using agenda_telefonica_back.Repositories.Interfaces;

namespace agenda_telefonica_back.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AppDbContext _context;

        public ContatoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contato>> ObterTodosAsync()
        {
            return await _context.Contatos.OrderBy(c => c.nome.ToLower()).ToListAsync();
        }

        public async Task<Contato> ObterPorIdAsync(int id)
        {
            return await _context.Contatos.FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task<Contato> CriarAsync(Contato contato)
        {
            _context.Contatos.Add(contato);
            await SalvarAsync();
            return contato;
        }

        public async Task<Contato> AtualizarAsync(Contato contato)
        {
            var contatoExistente = await _context.Contatos.FirstOrDefaultAsync(c => c.id == contato.id);

            if (contatoExistente == null)
                return null;

            contatoExistente.nome = contato.nome;
            contatoExistente.telefone = contato.telefone;

            _context.Contatos.Update(contatoExistente);
            await SalvarAsync();
            return contatoExistente;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var contato = await _context.Contatos.FirstOrDefaultAsync(c => c.id == id);

            if (contato == null)
                return false;

            _context.Contatos.Remove(contato);
            await SalvarAsync();
            return true;
        }

        public async Task<bool> SalvarAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
