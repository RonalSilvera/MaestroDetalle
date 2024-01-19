using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaestroDetalle.Models
{
    public class SubTareasDetalle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SubTareasDetalleId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Guid TareaId { get; set; }
        [ForeignKey("TareaId")]
        public virtual Tareas TareasNavigation { get; set; } = null!;
    }
}
