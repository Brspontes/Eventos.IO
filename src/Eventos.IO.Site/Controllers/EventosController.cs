using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eventos.IO.Application.VIEWMODELS;
using Eventos.IO.Site.Data;
using Eventos.IO.Application.INTERFACES;
using Eventos.IO.Domain.Core.NOTIFICATIONS;

namespace Eventos.IO.Site.Controllers
{
    public class EventosController : BaseController
    {
        private readonly IEventoAppService _eventoAppService;

        public EventosController(IEventoAppService eventoAppService,
                                 IDomainNotificationHandler<DomainNotification> notification) : base(notification)
        {
            _eventoAppService = eventoAppService;
        }

        public IActionResult Index()
        {
            return View(_eventoAppService.ObterTodos());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoViewModel = _eventoAppService.ObterEventoPorId(id.Value);
            if (eventoViewModel == null)
            {
                return NotFound();
            }

            return View(eventoViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventoViewModel eventoViewModel)
        {
            if (!ModelState.IsValid) return View(eventoViewModel);

            _eventoAppService.Registrar(eventoViewModel);

            if (OperacaoValida()) ViewBag.RetornoPost = "sucess,Evento registrado com sucesso";
            else ViewBag.RetornoPost = "error,Evento não registrado! verifique as mensagens";

            return View(eventoViewModel);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoViewModel = _eventoAppService.ObterEventoPorId(id.Value);
            if (eventoViewModel == null)
            {
                return NotFound();
            }

            if (OperacaoValida()) ViewBag.RetornoPost = "sucess,Evento atualizado com sucesso";
            else ViewBag.RetornoPost = "error,Evento não pode ser atualizado! verifique as mensagens";

            return View(eventoViewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EventoViewModel eventoViewModel)
        {
            if (!ModelState.IsValid) return View(eventoViewModel);

            _eventoAppService.Atualizar(eventoViewModel);
            // TODO: Validar se a operação ocorreu com sucesso
            return View(eventoViewModel);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventoViewModel = _eventoAppService.ObterEventoPorId(id.Value);
            if (eventoViewModel == null)
            {
                return NotFound();
            }

            return View(eventoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _eventoAppService.Excluir(id);
            return Redirect("Index");
        }
    }
}
