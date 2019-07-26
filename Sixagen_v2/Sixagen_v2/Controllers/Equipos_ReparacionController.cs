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
    [Authorize]
    public class Equipos_ReparacionController : Controller
    {
        private Sixagenv2Entities db = new Sixagenv2Entities();

        // GET: Equipos_Reparacion Empleados
        [Authorize(Roles = "Empleado")]
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["ID"]);
            //var equipos_Reparacion = db.Equipos_Reparacion.Include(e => e.Clientes).Include(e => e.Empleados);
            var yonose = db.Equipos_Reparacion.Where(e => e.Empleado == id);
            
            return View(yonose.ToList());
        }

        // GET: Equipos_Reparacion Clientes
        [Authorize(Roles = "Cliente")]
        public ActionResult IndexC()
        {
            int id = Convert.ToInt32(Session["ID"]);
            //var equipos_Reparacion = db.Equipos_Reparacion.Include(e => e.Clientes).Include(e => e.Empleados);
            var yonose = db.Equipos_Reparacion.Where(e => e.Dueno == id);

            return View(yonose.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IndexA()
        {
            int id = Convert.ToInt32(Session["ID"]);
            var equipos_Reparacion = db.Equipos_Reparacion.Include(e => e.Clientes).Include(e => e.Empleados);

            return View(equipos_Reparacion.ToList());
        }

        // GET: Equipos_Reparacion/Details/5 Empleados
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos_Reparacion equipos_Reparacion = db.Equipos_Reparacion.Find(id);
            if (equipos_Reparacion == null)
            {
                return HttpNotFound();
            }
            return View(equipos_Reparacion);
        }

        // GET: Equipos_Reparacion/Details/5 Clientes
        public ActionResult DetailsC(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos_Reparacion equipos_Reparacion = db.Equipos_Reparacion.Find(id);
            if (equipos_Reparacion == null)
            {
                return HttpNotFound();
            }
            return View(equipos_Reparacion);
        }

        // GET: Equipos_Reparacion/Create
        public ActionResult Create()
        {
            ViewBag.Dueno = new SelectList(db.Clientes, "ID", "Nombre");
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre");
            return View();
        }

        // POST: Equipos_Reparacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre_Equipo,Descripcion,Dueno,Empleado,Problema,Estado,Fecha_Entrada")] Equipos_Reparacion equipos_Reparacion)
        {
            if (ModelState.IsValid)
            {
                db.Equipos_Reparacion.Add(equipos_Reparacion);
                db.SaveChanges();
                return RedirectToAction("IndexA");
            }

            ViewBag.Dueno = new SelectList(db.Clientes, "ID", "Nombre", equipos_Reparacion.Dueno);
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre", equipos_Reparacion.Empleado);
            return View(equipos_Reparacion);
        }

        // GET: Equipos_Reparacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos_Reparacion equipos_Reparacion = db.Equipos_Reparacion.Find(id);
            if (equipos_Reparacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dueno = new SelectList(db.Clientes, "ID", "Nombre", equipos_Reparacion.Dueno);
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre", equipos_Reparacion.Empleado);
            return View(equipos_Reparacion);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditA(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos_Reparacion equipos_Reparacion = db.Equipos_Reparacion.Find(id);
            if (equipos_Reparacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dueno = new SelectList(db.Clientes, "ID", "Nombre", equipos_Reparacion.Dueno);
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre", equipos_Reparacion.Empleado);
            return View(equipos_Reparacion);
        }

        // POST: Equipos_Reparacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre_Equipo,Descripcion,Dueno,Empleado,Problema,Estado,Fecha_Entrada")] Equipos_Reparacion equipos_Reparacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipos_Reparacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Dueno = new SelectList(db.Clientes, "ID", "Nombre", equipos_Reparacion.Dueno);
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre", equipos_Reparacion.Empleado);
            return View(equipos_Reparacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditA([Bind(Include = "ID,Nombre_Equipo,Descripcion,Dueno,Empleado,Problema,Estado,Fecha_Entrada")] Equipos_Reparacion equipos_Reparacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipos_Reparacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexA");
            }
            ViewBag.Dueno = new SelectList(db.Clientes, "ID", "Nombre", equipos_Reparacion.Dueno);
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre", equipos_Reparacion.Empleado);
            return View(equipos_Reparacion);
        }

        // GET: Equipos_Reparacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipos_Reparacion equipos_Reparacion = db.Equipos_Reparacion.Find(id);
            if (equipos_Reparacion == null)
            {
                return HttpNotFound();
            }
            return View(equipos_Reparacion);
        }

        // POST: Equipos_Reparacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipos_Reparacion equipos_Reparacion = db.Equipos_Reparacion.Find(id);
            db.Equipos_Reparacion.Remove(equipos_Reparacion);
            db.SaveChanges();
            return RedirectToAction("IndexA");
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
