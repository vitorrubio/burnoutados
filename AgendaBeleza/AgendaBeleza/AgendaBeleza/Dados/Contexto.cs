using Microsoft.EntityFrameworkCore;
using AgendaBeleza.Dominio;

namespace AgendaBeleza.Dados
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        //public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Cliente> Clientes => Set<Cliente>();

    }
}
