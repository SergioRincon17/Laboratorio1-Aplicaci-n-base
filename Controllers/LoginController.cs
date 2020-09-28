using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio1_Aplicación_base.Models;

namespace Laboratorio1_Aplicación_base.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Crear_Cuenta(Usuario usuario) {
            try
            {
                if (ModelState.IsValid) //Comprueba que el modelo de Usuario o los modelos correspoendan 
                {
                    using (Laboratorio1_AP_DataBaseEntities DataBase = new Laboratorio1_AP_DataBaseEntities())
                    {
                        var DataBaseTabla_1 = DataBase.tablaUsuariosdB.Where(User => User.correo.Contains(usuario.Correo)).FirstOrDefault();
                        if (DataBaseTabla_1 != null)
                            return Redirect("~/Login/Index#signup");
                        else
                        {
                            var TempUsuario = new tablaUsuariosdB();
                            TempUsuario.password = usuario.Contraseña;
                            TempUsuario.correo = usuario.Correo;
                            TempUsuario.tipoDocumento = Request.Form["tipoDoc"];
                            TempUsuario.numeroDocumento = usuario.Documento;
                            TempUsuario.nombre = usuario.Nombre;
                            TempUsuario.rol = "Estudiante";
                            DataBase.tablaUsuariosdB.Add(TempUsuario);
                            DataBase.SaveChanges();
                        }
                    }
                    return Redirect("~/Login/Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                return Redirect("~/Login/Index#signup");
                throw new Exception(ex.Message);
            }
        }
        public ActionResult VerificarLogin(Usuario usuario)
        {
            try{
                using (Laboratorio1_AP_DataBaseEntities DataBase = new Laboratorio1_AP_DataBaseEntities())
                {
                    var DataBaseTabla_1 = DataBase.tablaUsuariosdB.Where(User => User.correo.Contains(usuario.Correo)).FirstOrDefault();
                    if (DataBaseTabla_1 != null){
                        if (DataBaseTabla_1.correo != null && DataBaseTabla_1.password == usuario.Contraseña)
                            return Redirect("~/Home/Index");
                    }
                }
                return Redirect("~/Login/Index");
            }
            catch (Exception ex)
            {
                return Redirect("~/Login/Index");
                throw new Exception(ex.Message);
            }
        }
    }
}
