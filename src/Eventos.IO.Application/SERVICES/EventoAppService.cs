using AutoMapper;
using Eventos.IO.Application.INTERFACES;
using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Domain.Core.BUS;
using Eventos.IO.Domain.EVENTOS.COMMANDS;
using Eventos.IO.Domain.EVENTOS.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.SERVICES
{
    public class EventoAppService : IEventoAppService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IEventoRepository _eventoRepository;

        public EventoAppService(IBus bus, IMapper mapper, IEventoRepository eventoRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _eventoRepository = eventoRepository;
        }

        public void Registrar(EventoViewModel eventoViewModel)
        {
            var registroCommand = _mapper.Map<RegistrarEventoCommand>(eventoViewModel);
            _bus.SendCommand(registroCommand);
        }

        public void Atualizar(EventoViewModel eventoViewModel)
        {
            // TODO: Validar se o organizador é dono do evento
            var atualizarEventoCommand = _mapper.Map<AtualizarEventoCommand>(eventoViewModel);
            _bus.SendCommand(atualizarEventoCommand);
        }

        public void Dispose()
        { _eventoRepository.Dispose(); }

        public void Excluir(Guid id)
        { _bus.SendCommand(new ExcluirEventoCommand(id)); }

        public EventoViewModel ObterEventoPorId(Guid id)
        { return _mapper.Map<EventoViewModel>(_eventoRepository.ObterPorId(id)); }

        public IEnumerable<EventoViewModel> ObterEventoPorOrganizador(Guid organizadorId)
        { return _mapper.Map<IEnumerable<EventoViewModel>>(_eventoRepository.ObterEventoPorOrganizador(organizadorId)); }

        public IEnumerable<EventoViewModel> ObterTodos()
        { return _mapper.Map<IEnumerable<EventoViewModel>>(_eventoRepository.ObterTodos()); }
    }
}
