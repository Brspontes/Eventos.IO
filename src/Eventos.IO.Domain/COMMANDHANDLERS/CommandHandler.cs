using Eventos.IO.Domain.Core.BUS;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.INTERFACES;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

using System.Text;

namespace Eventos.IO.Domain.COMMANDHANDLERS
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IBus _bus;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public CommandHandler(IUnitOfWork uow, IBus bus, IDomainNotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _bus = bus;
            _notifications = notifications;
        }

        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
                _bus.RaiseEvent(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }

        protected bool Commit()
        {
            if (_notifications.HasNotifications()) return false;

            var commandResponse = _uow.Commit();
            if (commandResponse.Success) return true;

            Console.WriteLine("Erro ao salvar os dados no banco");
            _bus.RaiseEvent(new DomainNotification("Commit", "Erro ao salvar os dados no banco"));
            return false;
        }
    }
}
