﻿using AutoMapper;
using Eventos.IO.Application.AUTOMAPPER;
using Eventos.IO.Application.INTERFACES;
using Eventos.IO.Application.SERVICES;
using Eventos.IO.Domain.Core.BUS;
using Eventos.IO.Domain.Core.EVENTS;
using Eventos.IO.Domain.Core.NOTIFICATIONS;
using Eventos.IO.Domain.EVENTOS.COMMANDS;
using Eventos.IO.Domain.EVENTOS.EVENTS;
using Eventos.IO.Domain.EVENTOS.REPOSITORY;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Domain.ORGANIZADORES.COMMANDS;
using Eventos.IO.Domain.ORGANIZADORES.EVENTS;
using Eventos.IO.Domain.ORGANIZADORES.REPOSITORY;
using Eventos.IO.Infra.CrossCuting.Bus;
using Eventos.IO.Infra.Data.CONTEXT;
using Eventos.IO.Infra.Data.REPOSITORY;
using Eventos.IO.Infra.Data.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Eventos.IO.Infra.CrossCuting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Application
            services.AddSingleton<IConfigurationProvider>(AutoMapperConfiguration.RegisterMappings());
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IEventoAppService, EventoAppService>();
            services.AddScoped<IOrganizadorAppService, OrganizadorAppService>();
            //DOMAIN Commands
            services.AddScoped<IHandler<RegistrarEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<AtualizarEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<ExcluirEventoCommand>, EventoCommandHandler>();
            services.AddScoped<IHandler<RegistrarOrganizadorCommand>, OrganizadorCommandHandler>();
            //Domain - Eventos
            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IHandler<EventoRegistradoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<EventoAtualizadoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<EventoExcluidoEvent>, EventoEventHandler>();
            services.AddScoped<IHandler<OrganizadorRegistradoEvent>, OrganizadorEventHandler>();
            //Infra - Data
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IOrganizadorRepository, OrganizadorRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<EventosContext>();
            //Infra Bus
            services.AddScoped<IBus, InMemoryBus>();
        }
    }
}
