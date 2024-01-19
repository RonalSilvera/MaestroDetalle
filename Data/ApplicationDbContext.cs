using MaestroDetalle.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MaestroDetalle.Data
{
    public class ApplicationDbContext:DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base (option)
        {
            
        }

        public DbSet<Tareas> Tareas { get; set; } 
        public DbSet<SubTareasDetalle> SubTareas { get; set; } 

        //ESTABLECER LA RELACION ENTRE MAESTRO Y DETALLE

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tareas>()
            .HasMany (p=>p.Detalles)
            .WithOne (d=>d.TareasNavigation)
            .HasForeignKey(d=>d.TareaId)
            .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
