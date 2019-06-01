using Eventos.IO.Application.VIEWMODELS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.INTERFACES
{
    public interface IOrganizadorAppService : IDisposable
    {
        void Registrar(OrganizadorViewModel organizadorViewModel);
    }
}
