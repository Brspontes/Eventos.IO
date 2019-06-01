using Eventos.IO.Domain.COMMANDHANDLERS;
using Eventos.IO.Domain.Core.BUS;
using Eventos.IO.Domain.Core.EVENTS;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Domain.ORGANIZADORES.REPOSITORY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eventos.IO.Domain.ORGANIZADORES.COMMANDS
{
    public class OrganizadorCommandHandler : CommandHandler, IHandler<RegistrarOrganizadorCommand>
    {
        private readonly IBus _bus;
        private readonly IOrganizadorRepository _organizadorRepository;

        public OrganizadorCommandHandler(
            IUnitOfWork uow, 
            IBus bus, 
            IDomainNotificationHandler<DomainNotification> notifications, 
            IOrganizadorRepository organizadorRepository) : base(uow, bus, notifications)
        {
            _bus = bus;
            _organizadorRepository = organizadorRepository;
        }

        public void Handle(RegistrarOrganizadorCommand message)
        {
            var organizador = new Organizador(message.Id, message.Nome, message.CPF, message.Email);
            if (!organizador.EhValido())
            {
                NotificarValidacoesErro(organizador.ValidationResult);
                return;
            }

            var organizadorExistente = _organizadorRepository.Buscar(o => o.CPF == organizador.CPF || o.Email == organizador.Email);

            if (organizadorExistente.Any())
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "CPF ou e-mail já utilizados"));

            _organizadorRepository.Adicionar(organizador);

            if(Commit())
            {
                //_bus.RaiseEvent();
            }
        }
    }
}
