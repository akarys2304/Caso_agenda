using agenda_telefonica_back.Models;
using agenda_telefonica_back.Models.DTOs;
using AutoMapper;

namespace agenda_telefonica_back.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento de Contato para ContatoDTO
            CreateMap<Contato, ContatoDTO>().ReverseMap();

            // Mapeamento de CriarContatoDTO para Contato
            CreateMap<CriarContatoDTO, Contato>();

            // Mapeamento de AtualizarContatoDTO para Contato
            CreateMap<AtualizarContatoDTO, Contato>();
        }
    }
}
