namespace agenda_telefonica_back.Models
{
    public class Contato
    {
        public int Id { get; set; }

        public required string nome { get; set; }

        public required string telefone { get; set; }
    }
}
