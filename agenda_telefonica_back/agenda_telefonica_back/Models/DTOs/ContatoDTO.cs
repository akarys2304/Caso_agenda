namespace agenda_telefonica_back.Models.DTOs
{
    public class CriarContatoDTO
    {
        public required string nome { get; set; }
        public required string telefone { get; set; }
    }

    /// <summary>
    /// DTO para atualização de um Produto (parcial)
    /// </summary>
    public class AtualizarContatoDTO
    {
        public required string nome { get; set; }
        public required string telefone { get; set; }
    }
    public class ContatoDTO
    {
        public int id { get; set; }
        public required string nome { get; set; }
        public required string telefone { get; set; }
    }
}
