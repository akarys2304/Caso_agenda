using agenda_telefonica_back.Models;
using agenda_telefonica_back.Models.DTOs;
using agenda_telefonica_back.Repositories.Interfaces;
using agenda_telefonica_back.Services.Interfaces;
using AutoMapper;

namespace agenda_telefonica_back.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ContatoService> _logger;

        public ContatoService(IContatoRepository contatoRepository, IMapper mapper, ILogger<ContatoService> logger)
        {
            _contatoRepository = contatoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ContatoDTO>> ObterTodosAsync()
        {
            try
            {
                _logger.LogInformation("Obtendo todos os contatos");
                var contatos = await _contatoRepository.ObterTodosAsync();
                return _mapper.Map<IEnumerable<ContatoDTO>>(contatos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter contatos: {ex.Message}");
                throw;
            }
        }

        public async Task<ContatoDTO> ObterPorIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Obtendo contato com Id: {id}");
                var contato = await _contatoRepository.ObterPorIdAsync(id);

                if (contato == null)
                {
                    _logger.LogWarning($"Contato com Id {id} não encontrado");
                    return null;
                }

                return _mapper.Map<ContatoDTO>(contato);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter contato {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<ContatoDTO> CriarAsync(CriarContatoDTO criarContatoDTO)
        {
            try
            {
                _logger.LogInformation($"Criando novo contato: {criarContatoDTO.nome}");

                var contato = _mapper.Map<Contato>(criarContatoDTO);
                var contatoCriado = await _contatoRepository.CriarAsync(contato);

                _logger.LogInformation($"Contato criado com sucesso. Id: {contatoCriado.id}");
                return _mapper.Map<ContatoDTO>(contatoCriado);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar produto: {ex.Message}");
                throw;
            }
        }

        public async Task<ContatoDTO> AtualizarAsync(int id, AtualizarContatoDTO atualizarContatoDTO)
        {
            try
            {
                _logger.LogInformation($"Atualizando contato com Id: {id}");

                var contatoExistente = await _contatoRepository.ObterPorIdAsync(id);

                if (contatoExistente == null)
                {
                    _logger.LogWarning($"Contato com Id {id} não encontrado para atualização");
                    return null;
                }

                _mapper.Map(atualizarContatoDTO, contatoExistente);
                var contatoAtualizado = await _contatoRepository.AtualizarAsync(contatoExistente);

                _logger.LogInformation($"Contato {id} atualizado com sucesso");
                return _mapper.Map<ContatoDTO>(contatoAtualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar contato {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Deletando contato com Id: {id}");
                var resultado = await _contatoRepository.DeletarAsync(id);

                if (!resultado)
                {
                    _logger.LogWarning($"Contato com Id {id} não encontrado para deleção");
                    return false;
                }

                _logger.LogInformation($"Contato {id} deletado com sucesso");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao deletar contato {id}: {ex.Message}");
                throw;
            }
        }
    }
}
