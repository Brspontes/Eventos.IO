using Dapper;
using Eventos.IO.Domain.EVENTOS;
using Eventos.IO.Domain.EVENTOS.REPOSITORY;
using Eventos.IO.Infra.Data.CONTEXT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eventos.IO.Infra.Data.REPOSITORY
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public EventoRepository(EventosContext context) : base(context)
        {

        }
        public override IEnumerable<Evento> ObterTodos()
        {
            var sql = "SELECT * FROM EVENTOS E " +
                      "WHERE E. EXCLUIDO = 0 " +
                      "ORDER BY E.DATAFIM DESC ";

            return Db.Database.GetDbConnection().Query<Evento>(sql);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            Db.Enderecos.Add(endereco);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            Db.Enderecos.Update(endereco);
        }

        public Endereco ObterEnderecoPorId(Guid id)
        {
            var sql = "SELECT * FROM ENDERECOS E " +
                      "WHERE E.ID = @UID";

            var endereco = Db.Database.GetDbConnection().Query<Endereco>(sql, new { UID = id });
            return endereco.SingleOrDefault();
        }

        public IEnumerable<Evento> ObterEventoPorOrganizador(Guid organizadorId)
        {
            var sql = "SELECT * FROM EVENTOS E " +
                      "WHERE E.EXCLUIDO = 0 " +
                      "AND E.ORGANIZADORID = @OID " +
                      "ORDER BY E.DATAFIM DESC";

            return Db.Database.GetDbConnection().Query<Evento>(sql, new { @OID = organizadorId });
        }

        public override Evento ObterPorId(Guid id)
        {
            var sql = "SELECT * FROM EVENTOS " +
                      "LEFT JOIN ENDERECOS EN " +
                      "ON E.ID = EN.EVENTOID " +
                      "WHERE E.ID = @UID";

            var evento = Db.Database.GetDbConnection().Query<Evento, Endereco, Evento>(sql,
                (e, en) =>
                {
                    if (en != null)
                        e.AtribuirEndereco(en);
                    return e;
                }, new { UID = id });

            return evento.FirstOrDefault();
        }
    }
}
