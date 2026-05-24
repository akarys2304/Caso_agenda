using agenda_telefonica_back.Models;
using agenda_telefonica_back.Models.DTOs;
using agenda_telefonica_back.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace agenda_telefonica_back.Controllers
{
    [ApiController]
    [Route("api/contatos")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;
        private readonly ILogger<ContatoController> _logger;

        public ContatoController(IContatoService contatoService, ILogger<ContatoController> logger) { 
            _contatoService = contatoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoDTO>>> ObterTodos()
        {
            _logger.LogInformation("GET /api/contatos - Listando todos os contatos");
            var contatos = await _contatoService.ObterTodosAsync();
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoDTO>> ObterPorId(int id)
        {
            _logger.LogInformation($"GET /api/contatos/{id} - Obtendo contato específico");
            var contato = await _contatoService.ObterPorIdAsync(id);

            if (contato == null)
            {
                _logger.LogWarning($"Produto com Id {id} não encontrado");
                return NotFound(new { mensagem = $"Produto com Id {id} não encontrado" });
            }

            return Ok(contato);
        }

        [HttpPost]
        public async Task<ActionResult<ContatoDTO>> Criar([FromBody] CriarContatoDTO criarContatoDTO)
        {
            _logger.LogInformation($"POST /api/contatos - Criando novo contato");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var contatoCriado = await _contatoService.CriarAsync(criarContatoDTO);

                // Retorna 201 Created com a localização do novo recurso
                return CreatedAtAction(nameof(ObterPorId), new { id = contatoCriado.id }, contatoCriado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContatoDTO>> Atualizar(int id, [FromBody] AtualizarContatoDTO atualizarContatoDTO)
        {
            _logger.LogInformation($"PUT /api/contatos/{id} - Atualizando contato");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var contatoAtualizado = await _contatoService.AtualizarAsync(id, atualizarContatoDTO);

                if (contatoAtualizado == null)
                {
                    return NotFound(new { mensagem = $"Contato com Id {id} não encontrado" });
                }

                return Ok(contatoAtualizado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            _logger.LogInformation($"DELETE /api/contatos/{id} - Deletando contato");

            var resultado = await _contatoService.DeletarAsync(id);

            if (!resultado)
            {
                return NotFound(new { mensagem = $"Contato com Id {id} não encontrado" });
            }

            // 204 No Content - indica sucesso mas sem corpo na resposta
            return NoContent();
        }
    }
}
