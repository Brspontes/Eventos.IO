using Eventos.IO.Domain.Core.BUS;
using Eventos.IO.Domain.Core.COMMANDS;
using Eventos.IO.Domain.Core.EVENTS;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.EVENTOS;
using Eventos.IO.Domain.EVENTOS.COMMANDS;
using Eventos.IO.Domain.EVENTOS.EVENTS;
using Eventos.IO.Domain.EVENTOS.REPOSITORY;
using Eventos.IO.Domain.INTERFACES;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new FakeBus();

            //Registro com sucesso
            var cmd = new RegistrarEventoCommand("DevX", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), true, 0, true, "Empresa");

            cmd = new RegistrarEventoCommand("", DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), false, 0, false, "");



            Console.ReadKey();
        }

        public class FakeBus : IBus
        {

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
                var msgType = message.MessageType;

                if(msgType.Equals("DomainNotification"))
                {
                    var obj = new DomainNotificationHandler();
                    ((IDomainNotificationHandler<T>)obj).Handle(message);
                }

                if(msgType.Equals("RegistrarEventoCommand") ||
                    msgType.Equals("AtualizarEventoCommand") ||
                    msgType.Equals("ExcluirEventoCommand"))
                {
                    var obj = new EventoCommandHandler(new FakeEventoRepository(), new FakeUow(), new FakeBus(), new DomainNotificationHandler());
                    ((IHandler<T>)obj).Handle(message);
                }

                if(msgType.Equals("EventoRegistradoEvent") ||
                   msgType.Equals("EventoAtualizadoEvent") ||
                   msgType.Equals("EventoExcluidoEvent"))
                {
                    var obj = new EventoEventHandler();
                    ((IHandler<T>)obj).Handle(message);
                }
            }
        }

        public class FakeEventoRepository : IEventoRepository
        {
            public void Add(Evento obj)
            {
               
            }

            public void Dispose()
            {
                
            }

            public IEnumerable<Evento> Find(Expression<Func<Evento, bool>> predicate)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Evento> GetAll()
            {
                throw new NotImplementedException();
            }

            public Evento GetById(Guid id)
            {
                return new Evento("Fake", DateTime.Now, DateTime.Now, true, 0, true, "Empresa");
            }

            public void Remove(Guid id)
            {
                
            }

            public int SaveChanges()
            {
                throw new NotImplementedException();
            }

            public void Update(Evento obj)
            {
                
            }
        }

        public class FakeUow : IUnitOfWork
        {
            public CommandResponse Commit()
            {
                return new CommandResponse(true);
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

    }
}