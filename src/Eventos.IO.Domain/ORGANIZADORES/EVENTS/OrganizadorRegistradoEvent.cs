using Eventos.IO.Domain.Core.EVENTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.ORGANIZADORES.EVENTS
{
    public class OrganizadorRegistradoEvent : Event
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }


        public OrganizadorRegistradoEvent(Guid id, string nome, string cPF, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cPF;
            Email = email;
        }
    }
}
