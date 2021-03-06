﻿using Eventos.IO.Domain.COMMANDHANDLERS;
using Eventos.IO.Domain.Core.BUS;
using Eventos.IO.Domain.Core.EVENTS;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.EVENTOS.EVENTS;
using Eventos.IO.Domain.EVENTOS.REPOSITORY;
using Eventos.IO.Domain.INTERFACES;
using System;


namespace Eventos.IO.Domain.EVENTOS.COMMANDS
{
    public class EventoCommandHandler : CommandHandler,
        IHandler<RegistrarEventoCommand>,
        IHandler<AtualizarEventoCommand>,
        IHandler<ExcluirEventoCommand>
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IBus _bus;


        public EventoCommandHandler(IEventoRepository eventoRepository, IUnitOfWork uow, IBus bus, IDomainNotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _eventoRepository = eventoRepository;
            _bus = bus;
        }

        public void Handle(RegistrarEventoCommand message)
        {
            var endereco = new Endereco(message.Endereco.Id, message.Endereco.Logradouro, message.Endereco.Numero, message.Endereco.Complemento, message.Endereco.Bairro,
                message.Endereco.CEP, message.Endereco.Cidade, message.Endereco.Estado, message.Id);

            var evento = Evento.EventoFactory.NovoEventoCompleto(message.Id, message.Nome, message.DescricaoCurta, message.DescricaoLonga, message.DataInicio, message.DataFim,
                message.Gratuito, message.Valor, message.Online, message.NomeEmpresa, message.OrganizadorId, endereco, message.CategoriaId);

            if (!EventoValido(evento)) return;

            // TODO:
            //Validacoes de negocio
            //Organizador pode registrar evento?

            //Persistencia
            _eventoRepository.Adicionar(evento);

            if (Commit())
            {
                Console.WriteLine("Evento registrado com sucesso");
                _bus.RaiseEvent(new EventoRegistradoEvent(evento.Id, evento.Nome, evento.DataInicio, evento.DataFim,
                    evento.Gratuito, evento.Valor, evento.Online, evento.NomeEmpresa));
            }
        }

        public void Handle(ExcluirEventoCommand message)
        {
            if (!EventoExistente(message.Id, message.MessageType)) return;
            _eventoRepository.Remover(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new EventoExcluidoEvent(message.Id));
            }
        }

        public void Handle(AtualizarEventoCommand message)
        {
            var eventoAtual = _eventoRepository.ObterPorId(message.Id);
            if (!EventoExistente(message.Id, message.MessageType)) return;

            //TODO: Validar se o evento pertence a pessoa que está editando.

            var evento = Evento.EventoFactory.NovoEventoCompleto(message.Id, message.Nome, message.DescricaoCurta, message.DescricaoLonga,
                                                                  message.DataInicio, message.DataFim, message.Gratuito, message.Valor,
                                                                  message.Online, message.NomeEmpresa, null, eventoAtual.Endereco, message.CategoriaId);

            if (!EventoValido(evento)) return;

            _eventoRepository.Atualizar(evento);
            if (Commit())
            {
                _bus.RaiseEvent(new EventoAtualizadoEvent(evento.Id, evento.Nome, evento.DescricaoCurta, evento.DescricaoLonga, evento.DataInicio, evento.DataFim,
                    evento.Gratuito, evento.Valor, evento.Online, evento.NomeEmpresa));
            }
        }

        private bool EventoValido(Evento evento)
        {
            if (evento.EhValido()) return true;

            NotificarValidacoesErro(evento.ValidationResult);
            return false;
        }

        private bool EventoExistente(Guid id, string messageType)
        {
            var evento = _eventoRepository.ObterPorId(id);
            if (evento != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Evento não encontrado"));
            return false;
        }
    }
}
