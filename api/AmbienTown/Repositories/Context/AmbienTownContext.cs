using AmbienTown.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AmbienTown.Repositories.Context
{
    public class AmbienTownContext : DbContext
    {
        public static readonly ILoggerFactory DbCommandDebugLoggerFactory = LoggerFactory.Create(config =>
        {
            config.AddDebug();
        });

        public AmbienTownContext(DbContextOptions<AmbienTownContext> options) : base(options)
        {

        }

        public DbSet<Configuracao> Configuracoes { get; set; }
        public DbSet<Personagem> Personagens { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ProgressoMapa> ProgressosMapa { get; set; }       
    }
}