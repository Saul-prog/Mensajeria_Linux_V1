
using Microsoft.EntityFrameworkCore;
using Mensajeria_Linux.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mensajeria_Linux.EntityFramework.Data
{
    /// <summary>
    /// Contexto de la base de datos de la aplicación
    /// </summary>
    public class NotificationContext : DbContext
    {


        protected readonly IConfiguration _configuration;
        /// <summary>
        /// Constructor del contexto de la base de datos
        /// </summary>
        /// <param name="configuration"></param>
        public NotificationContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Tabla de Agencias
        /// </summary>
        public DbSet<Agencia> Agencias { get; set; }
        /// <summary>
        /// Tabla de Información de Teams
        /// </summary>
        public DbSet<InfoTeams> infoTeams { get; set; }
        /// <summary>
        /// Tabla de información de WhatsApp
        /// </summary>
        public DbSet<InfoWhatsApp> infoWhatsApp { get; set; }
        /// <summary>
        /// Tabla de información de Email
        /// </summary>
        public DbSet<InfoEmail>    infoEmail { get; set; }
        /// <summary>
        /// Tabla de información de SMS
        /// </summary>
        public DbSet<InfoSMS> infoSMS { get; set; }
        /// <summary>
        /// Tabla de administradores
        /// </summary>
        public DbSet<Administradores> administradores { get; set; }
        /// <summary>
        /// Tabla de plantillas
        /// </summary>
        public DbSet<Plantillas> Plantilla { get; set; }
        /// <summary>
        /// Se especifica de dónde se obtiene las credenciales y localización de la base de datos que se va a usar
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
        }
        /// <summary>
        /// Relaciones entre las tablas y caracteristicas de estas
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agencia>()
                .HasMany(t => t.InfoTeams)
                .WithOne(a => a.agencia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Agencia>()
                .HasMany(t => t.InfoWhatsApps)
                .WithOne(a => a.agencia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Agencia>()
                .HasMany(e => e.InfoEmail)
                .WithOne(a => a.agencia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Agencia>()
                .HasMany(e => e.InfoSMS)
                .WithOne(a => a.agencia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Agencia>()
                .HasMany(e => e.Plantillas) 
                .WithOne(a => a.agencia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Plantillas>()
                .Property(e => e.plantillaJSON)
                .HasColumnType("bytea");
            modelBuilder.Entity<Administradores>()
                .HasMany(e => e.agencias)
                .WithOne(a => a.adminsitrador)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
