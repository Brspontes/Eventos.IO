using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.EVENTOS.EVENTS
{
    public class EventoExcluidoEvent : BaseEventoEvent
    {
        public EventoExcluidoEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
