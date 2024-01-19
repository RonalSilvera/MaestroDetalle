using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaestroDetalle.Models
{
    public class Tareas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TareaId { get; set; }

        public Tareas() 
        {
            Detalles = new List<SubTareasDetalle> ();
        } 

        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        
        public List<SubTareasDetalle> Detalles { get; set; } = null!;

    }
}
