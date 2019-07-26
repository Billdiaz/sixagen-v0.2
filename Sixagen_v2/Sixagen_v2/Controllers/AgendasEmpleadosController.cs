using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sixagen_v2;

namespace Sixagen_v2.Controllers
{
    public class AgendasEmpleadosController : Controller
    {
        private Sixagenv2Entities db = new Sixagenv2Entities();
        [Authorize(Roles = "Empleado")]
        // GET: AgendasEmpleados
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["ID"]);
          //  var idreal = db.Empleados.Where(e => e.IDUsuario == id);

            var yonose = db.Agendas.Where(e => e.Empleado == id);

            return View(yonose.ToList());
        
        }

        // GET: AgendasEmpleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendas agendas = db.Agendas.Find(id);
            if (agendas == null)
            {
                return HttpNotFound();
            }
            return View(agendas);
        }

        // GET: AgendasEmpleados/Create
        public ActionResult Create()
        {
            ViewBag.Cliente = new SelectList(db.Clientes, "ID", "Nombre");
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
