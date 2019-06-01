using Eventos.IO.Domain.ORGANIZADORES;
using Eventos.IO.Domain.ORGANIZADORES.REPOSITORY;
using Eventos.IO.Infra.Data.CONTEXT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Infra.Data.REPOSITORY
{
    public class OrganizadorRepository : Repository<Organizador>, IOrganizadorRepository
    {
        public OrganizadorRepository(EventosContext context) : base(context)
        {
        }
    }
}
