﻿using Eventos.IO.Domain.Core.BUS;
using Eventos.IO.Domain.Core.COMMANDS;
using Eventos.IO.Domain.Core.EVENTS;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using Eventos.IO.Domain.Core.NOTIFICATIONS;

namespace Eventos.IO.Infra.CrossCuting.Bus
{
    public sealed class InMemoryBus : IBus
    {
        public static Func<IServiceProvider> ContainerAccessor { get; set; }
        private static IServiceProvider Container => ContainerAccessor();

        public void RaiseEvent<T>(T theEvent) where T : Event
        {
            Publish(theEvent);
        }

        public void SendCommand<T>(T theCommand) where T : Command
        {
            Publish(theCommand);
        }

        private static void Publish<T>(T message) where T : Message
        {
            if (Container == null) return;

            var obj = Container.GetService(message.MessageType.Equals("DomainNotification")
                ? typeof(IDomainNotificationHandler<T>)
                : typeof(IHandler<T>));

            ((IHandler<T>)obj).Handle(message);
        }
    }
}
