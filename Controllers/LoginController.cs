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
                    using (Base_Datos_App1Entities DataBase = new Base_Datos_App1Entities())
                    {
                        var DataBaseTabla_1 = DataBase.Tabla_1.Where(User => User.Correo.Contains(usuario.Correo)).FirstOrDefault();
                        if (DataBaseTabla_1 != null)
                            return Redirect("~/Login/Index#signup");
                        else
                        {
                            var TempUsuario = new Tabla_1();
                            TempUsuario.Contraseña = usuario.Contraseña;
                            TempUsuario.Correo = usuario.Correo;
                            TempUsuario.Tipo_Documento = Request.Form["tipoDoc"];
                            TempUsuario.Documento = usuario.Documento;
                            TempUsuario.Nombre = usuario.Nombre;
                            TempUsuario.Rol = "Estudiante";
                            DataBase.Tabla_1.Add(TempUsuario);
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
                using (Base_Datos_App1Entities DataBase = new Base_Datos_App1Entities())
                {
                    var DataBaseTabla_1 = DataBase.Tabla_1.Where(User => User.Correo.Contains(usuario.Correo)).FirstOrDefault();
                    if (DataBaseTabla_1 != null){
                        if (DataBaseTabla_1.Correo != null && DataBaseTabla_1.Contraseña == usuario.Contraseña)
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
