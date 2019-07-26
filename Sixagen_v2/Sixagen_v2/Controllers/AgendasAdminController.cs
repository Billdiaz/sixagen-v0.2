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
    [Authorize(Roles = "Admin")]
    public class AgendasAdminController : Controller
    {
        private Sixagenv2Entities db = new Sixagenv2Entities();

        // GET: AgendasAdmin
        public ActionResult Index()
        {
            var agendas = db.Agendas.Include(a => a.Clientes).Include(a => a.Empleados);
            return View(agendas.ToList());
        }
      

        // GET: AgendasAdmin/Details/5
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

        // GET: AgendasAdmin/Create
        public ActionResult Create()
        {
            ViewBag.Cliente = new SelectList(db.Clientes, "ID", "Nombre");
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre");
            return View();
        }

        // POST: AgendasAdmin/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Motivo,Descripcion,Cliente,Empleado,Fecha,Hora,Herramientas_Necesarias")] Agendas agendas)
        {
            if (ModelState.IsValid)
            {
                db.Agendas.Add(agendas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente = new SelectList(db.Clientes, "ID", "Nombre", agendas.Cliente);
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre", agendas.Empleado);
            return View(agendas);
        }

        // GET: AgendasAdmin/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Cliente = new SelectList(db.Clientes, "ID", "Nombre", agendas.Cliente);
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre", agendas.Empleado);
            return View(agendas);
        }

        // POST: AgendasAdmin/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Motivo,Descripcion,Cliente,Empleado,Fecha,Hora,Herramientas_Necesarias")] Agendas agendas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agendas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente = new SelectList(db.Clientes, "ID", "Nombre", agendas.Cliente);
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre", agendas.Empleado);
            return View(agendas);
        }

        // GET: AgendasAdmin/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: AgendasAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agendas agendas = db.Agendas.Find(id);
            db.Agendas.Remove(agendas);
            db.SaveChanges();
            return RedirectToAction("Index");
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
