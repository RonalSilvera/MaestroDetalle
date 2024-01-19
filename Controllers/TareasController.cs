using MaestroDetalle.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaestroDetalle.Data;

namespace MaestroDetalle.Controllers
{
    //CRUD DEL MAESTRO
    public class TareasController : Controller
    {
        //propiedad privada de applicationDbContext llamada _context
        private readonly ApplicationDbContext _context;

        //CONEXION EN EL CONSTRUCTOR DEL CONTROLADOR Y EL DBCONTEXT
        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        //metodo index - listar tareas

        public ActionResult Index()
        {
            var tareas = _context.Tareas.Include(p=>p.Detalles).ToList();

            return View(tareas);
           
        }

        //METODO GET PARA CREAR, CUANDO EL USUARIO NAVEGA A lA URL /TAREAS/CREATE SE LLAMA A ESTE METODO, SU FUNCION PRINCIPAL ES MOSTRAR EL FORMULARIO PARA CREAR NUEVA TAREA

        public ActionResult Create ()
        {
            return View(); 
        }

        //METODO POST PARA CREAR

        [HttpPost]
        [ValidateAntiForgeryToken] //ATRIBUTO DE SEGURIDAD QUE AYUDA A PREVENIR ATAQUES CSRF(cross-site-requestforgery)
        public ActionResult Create(Tareas tareas) 
        {
            if (ModelState.IsValid)
            {
                _context.Tareas.Add(tareas);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View (tareas);
        }

        public ActionResult Details(Guid? id)
        {
           if (id == null)
            {
                return NotFound();
            }

           var tarea = _context.Tareas.Include(t => t.Detalles) // utiliza la carga ansiosa para incluir las subtareas asociadas a la tarea en consulta
                .FirstOrDefault(t => t.TareaId == id); //obtiene la primera que coincide con el id proporcionado. si no se encuentra ninguna
                                                       //tarea sera nulo

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        //GET
        
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();  
            }

            var tarea = _context.Tareas.FirstOrDefault(t => t.TareaId == id); //utiliza el contexto de la base de datos para buscar el id proporcionado

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

       //POST
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (Guid id, Tareas tareas) 
        { 
            if (id != tareas.TareaId)
                return NotFound();

                _context.Update(tareas);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //utiliza el contexto de la base de datos para encontrar el id proporcionado
            var tarea = _context.Tareas.FirstOrDefault(t => t.TareaId == id);

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfi(Guid id) //representa el id de la tarea que se esta eliminando
        {
            var tarea = _context.Tareas.Find(id); //se utiliza el metodo find para buscar la tarea con el id en la base de datos

            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //CRUD DETALLE

        public IActionResult EditDetalle(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtarea = _context.SubTareas.FirstOrDefault(st => st.SubTareasDetalleId == id);

            if (subtarea == null)
            {
                return NotFound();
            }

            return View(subtarea);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDetalle(Guid id, SubTareasDetalle subtarea)
        {
 
            if (id != subtarea.SubTareasDetalleId) // verifica si el id proporcionado en la url coincide con el id de la subtarea en el objeto subtarea
                return NotFound();

            _context.Update(subtarea);
            _context.SaveChanges();


            return RedirectToAction(nameof(Details), new { id = subtarea.TareaId }); //se retorna a la pagina anterior details del controlador tarea, pasando el id de la tarea a la que pertenece la subtarea,
                                                                                     //esto llevara de vuelta a la vista de detalles de la tarea
        }

        public IActionResult DeleteDetalle(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtarea = _context.SubTareas.FirstOrDefault(st => st.SubTareasDetalleId == id);

            if (subtarea == null)
            {
                return NotFound();
            }

            return View(subtarea);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDetalleConfi(Guid id)
        {
            var subtarea = _context.SubTareas.Find(id);

            if (subtarea == null)
            {
                return NotFound();
            }

            _context.SubTareas.Remove(subtarea);
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = subtarea.TareaId });
        }










    }
}
