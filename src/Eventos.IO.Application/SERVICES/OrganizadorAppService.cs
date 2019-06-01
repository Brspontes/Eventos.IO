using AutoMapper;
using Eventos.IO.Application.INTERFACES;
using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Domain.Core.BUS;
using Eventos.IO.Domain.ORGANIZADORES.COMMANDS;
using Eventos.IO.Domain.ORGANIZADORES.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.SERVICES
{
    public class OrganizadorAppService : IOrganizadorAppService
    {
        private readonly IMapper _mapper;
        private readonly IOrganizadorRepository _organizadorRepository;
        private readonly IBus _bus;

        public OrganizadorAppService(IMapper mapper, IOrganizadorRepository organizadorRepository, IBus bus)
        {
            _mapper = mapper;
            _organizadorRepository = organizadorRepository;
            _bus = bus;
        }

        public void Dispose()
        {
            _organizadorRepository.Dispose();
        }

        public void Registrar(OrganizadorViewModel organizadorViewModel)
        {
            var registroCommand = _mapper.Map<RegistrarOrganizadorCommand>(organizadorViewModel);
            _bus.SendCommand(registroCommand);
        }
    }
}
