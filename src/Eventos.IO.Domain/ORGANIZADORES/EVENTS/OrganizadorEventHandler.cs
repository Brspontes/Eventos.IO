using Eventos.IO.Domain.Core.EVENTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.ORGANIZADORES.EVENTS
{
    public class OrganizadorEventHandler :
        IHandler<OrganizadorRegistradoEvent>
    {
        public void Handle(OrganizadorRegistradoEvent message)
        {
            //TODO: Enviar um email?
        }
    }
}
