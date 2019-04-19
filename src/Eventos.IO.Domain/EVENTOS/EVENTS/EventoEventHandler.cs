using Eventos.IO.Domain.Core.EVENTS;

namespace Eventos.IO.Domain.EVENTOS.EVENTS
{
    public class EventoEventHandler :
        IHandler<EventoRegistradoEvent>,
        IHandler<EventoAtualizadoEvent>,
        IHandler<EventoExcluidoEvent>
    {
        public void Handle(EventoExcluidoEvent message)
        {
            //Enviar Email
        }

        public void Handle(EventoAtualizadoEvent message)
        {
            //Enviar Email
        }

        public void Handle(EventoRegistradoEvent message)
        {
            //Enviar Email
        }
    }
}
