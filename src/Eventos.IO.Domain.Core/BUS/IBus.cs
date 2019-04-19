using Eventos.IO.Domain.Core.COMMANDS;
using Eventos.IO.Domain.Core.EVENTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Core.BUS
{
    public interface IBus
    {
        void SendCommand<T>(T theCommand) where T : Command;
        void RaiseEvent<T>(T theEvent) where T : Event;
    }
}
