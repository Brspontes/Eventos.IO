﻿using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Domain.EVENTOS;
using System;
using System.Collections.Generic;

namespace Eventos.IO.Domain.ORGANIZADORES
{
    public class Organizador : Entity<Organizador>
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }

        public Organizador(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }

        //EF construtor
        protected Organizador() { }

        //EF propriedade de navegação
        public virtual ICollection<Evento> Eventos{ get; set; }

        public override bool EhValido()
        {
            throw new System.NotImplementedException();
        }
    }
}