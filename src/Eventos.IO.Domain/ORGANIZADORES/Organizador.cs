﻿using Eventos.IO.Domain.Core.Models;
using System;

namespace Eventos.IO.Domain.ORGANIZADORES
{
    public class Organizador : Entity<Organizador>
    {
        public Organizador(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            throw new System.NotImplementedException();
        }
    }
}