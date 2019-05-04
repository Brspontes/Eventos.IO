using Eventos.IO.Domain.EVENTOS;
using Eventos.IO.Domain.ORGANIZADORES;
using Eventos.IO.Infra.Data.EXTENSIONS;
using Eventos.IO.Infra.Data.MAPPINGS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Eventos.IO.Infra.Data.CONTEXT
{
    public class EventosContext : DbContext
    {
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentAPI
            //EVENTOS
            modelBuilder.AddConfiguration(new EventoMapping());
            //ENDERECO
            modelBuilder.AddConfiguration(new EnderecoMapping());
            //ORGANIZADOR
            modelBuilder.AddConfiguration(new OrganizadorMapping());
            //CATEGORIA
            modelBuilder.AddConfiguration(new CategoriaMapping());

            #endregion
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
