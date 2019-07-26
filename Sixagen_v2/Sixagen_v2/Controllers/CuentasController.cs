using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sixagen_v2.Models;
using System.Web.Security;

namespace Sixagen_v2.Controllers
{
    
    [Authorize]
    public class CuentasController : Controller
    {
        private static int idcuenta = 0;
        // GET: Cuentas
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Cuentas c)
        {
            using (var db = new Sixagenv2Entities())
            {
                bool valido = db.Login.Any(a => a.Usuario == c.Usuario && a.Clave == c.Clave);
                if (valido)
                {
                    FormsAuthentication.SetAuthCookie(c.Usuario, false);

                    var res = (from r in db.Roles
                               join u in db.Login on r.IDUsuario equals u.ID
                               where u.Usuario == c.Usuario
                               select r.Role).ToArray();



                    switch (res[0])
                    {
                        case "Admin":

                            var id = (from e in db.Empleados
                                      join u in db.Login on e.IDUsuario equals u.ID
                                      where u.Usuario == c.Usuario
                                      select e);

                            foreach (var item in id)
                            {
                                Session["ID"] = item.ID;
                            }



                            return RedirectToAction("Index", "Empleados");
                            break;

                        case "Empleado":

                            var ide = (from e in db.Empleados
                                       join u in db.Login on e.IDUsuario equals u.ID
                                       where u.Usuario == c.Usuario
                                       select e);

                            foreach (var item in ide)
                            {
                                Session["ID"] = item.ID;
                            }

                            return RedirectToAction("Index", "Equipos_Reparacion");
                            break;

                        case "Cliente":

                            var idc = (from cl in db.Clientes
                                       join u in db.Login on cl.IDUsuario equals u.ID
                                       where u.Usuario == c.Usuario
                                       select cl);

                            foreach (var item in idc)
                            {
                                Session["ID"] = item.ID;
                            }

                            return RedirectToAction("Index", "HomeClientes");
                            break;

                    }

                }

                ModelState.AddModelError("", "Usuario o contraseña incorrecto");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Registrar()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registrar(Login c)
        {
            using (var db = new Sixagenv2Entities())
            {
                db.Login.Add(c);
                db.SaveChanges();

                var id = from i in db.Login
                         where i.Usuario == c.Usuario && i.Clave == c.Clave
                         select i.ID;

                foreach (var valor in id)
                {
                    idcuenta = valor;
                }
                return RedirectToAction("RegistrarCliente");
            }
            
        }

        [AllowAnonymous]
        public ActionResult RegistrarCliente()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegistrarCliente(Clientes c)
        {
            using (var db = new Sixagenv2Entities())
            {
                db.Clientes.Add(new Clientes {
                    ID = c.ID,
                    Nombre = c.Nombre,
                    Direccion = c.Direccion,
                    Telefono = c.Telefono,
                    Email = c.Email,
                    IDUsuario = idcuenta

                });

                db.Roles.Add(new Roles
                {
                    Role = "Cliente",
                    IDUsuario = idcuenta
                });

                db.SaveChanges();


                return RedirectToAction("Login");
            }

        }


        [Authorize(Roles = "Admin")]
        public ActionResult RegistrarE()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RegistrarE(Login c)
        {
            using (var db = new Sixagenv2Entities())
            {
                db.Login.Add(c);
                db.SaveChanges();

                var id = from i in db.Login
                         where i.Usuario == c.Usuario && i.Clave == c.Clave
                         select i.ID;

                foreach (var valor in id)
                {
                    idcuenta = valor;
                }
                return RedirectToAction("RegistrarEmpleado");
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult RegistrarEmpleado()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RegistrarEmpleado(Empleados e)
        {
            using (var db = new Sixagenv2Entities())
            {
                db.Empleados.Add(new Empleados
                {
                    ID = e.ID,
                    Nombre = e.Nombre,
                    Puesto = e.Puesto,
                    Fecha_Contratacion = e.Fecha_Contratacion,
                    Telefono = e.Telefono,
                    Email = e.Email,
                    IDUsuario = idcuenta

                });

                db.Roles.Add(new Roles
                {
                    Role = "Empleado",
                    IDUsuario = idcuenta
                });

                db.SaveChanges();


                return RedirectToAction("Index", "Empleados");
            }

        }

        public ActionResult Cerrar()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }
}