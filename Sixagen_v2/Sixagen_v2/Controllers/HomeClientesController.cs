using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sixagen_v2;

namespace Sixagen_v2.Controllers
{
    public class HomeClientesController : Controller
    {
        private Sixagenv2Entities db = new Sixagenv2Entities();
        // GET: HomeClientes
        [Authorize(Roles = "Cliente")]
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["ID"]);
       
            var yonose = db.Agendas.Where(e => e.Cliente == id);

            return View(yonose.ToList());
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
                int id = Convert.ToInt32(Session["ID"]);
                agendas.Cliente = id;
                db.Agendas.Add(agendas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente = new SelectList(db.Clientes, "ID", "Nombre", agendas.Cliente);
            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Nombre", agendas.Empleado);
            return View(agendas);
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
                int id = Convert.ToInt32(Session["ID"]);
                agendas.Cliente = id;
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




    }
}