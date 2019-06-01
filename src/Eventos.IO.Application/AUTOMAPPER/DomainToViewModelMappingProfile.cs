using AutoMapper;
using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Domain.EVENTOS;
using Eventos.IO.Domain.ORGANIZADORES;

namespace Eventos.IO.Application.AUTOMAPPER
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Evento, EventoViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<Organizador, OrganizadorViewModel>();
        }
    }
}
