﻿using Eventos.IO.Domain.Core.COMMANDS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.EVENTOS.COMMANDS
{
    public class IncluirEnderecoEventoCommand : Command
    {
        public IncluirEnderecoEventoCommand(
            Guid id,
            string logradouro,
            string numero,
            string complemento,
            string bairro,
            string cep,
            string cidade,
            string estado,
            Guid? eventoId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            EventoId = eventoId;
        }

        public Guid Id { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? EventoId { get; private set; }
    }
}
