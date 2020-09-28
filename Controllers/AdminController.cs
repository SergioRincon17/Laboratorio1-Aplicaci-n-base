using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio1_Aplicación_base.Models;

namespace Laboratorio1_Aplicación_base.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin   
        public ActionResult Lista_Usuarios()
        {
            var Tem_List = new Lista_Usuarios_M(); // Creando el modelo 
            Tem_List.List_Usuarios = new List<Usuario>(); //intnciando o declarando una lista
            using (Base_Datos_App1Entities DataBase = new Base_Datos_App1Entities()){ //leer base de datos
                foreach( var Elemento in DataBase.Tabla_1)
                {
                    Tem_List.List_Usuarios.Add(new Usuario {
                    ID=Elemento.ID,
                    Contraseña= Elemento.Contraseña,
                    Correo = Elemento.Correo,
                    Documento=Elemento.Documento,
                    Tipo_Documento = Elemento.Tipo_Documento,
                    Nombre = Elemento.Nombre,
                    Rol = Elemento.Rol
                    });
                }

            }
            return View(Tem_List);
        }

        public ActionResult View_Editar(int ID) {
            try
            {
                var TemUsuario = new Usuario();
                using (Base_Datos_App1Entities DataBase = new Base_Datos_App1Entities())
                {
                    var Usuario_ID = DataBase.Tabla_1.Find(ID);
                    TemUsuario.ID = ID;
                    TemUsuario.Nombre = Usuario_ID.Nombre;
                    TemUsuario.Documento = Usuario_ID.Documento;
                    TemUsuario.Tipo_Documento = Usuario_ID.Tipo_Documento;
                    TemUsuario.Correo = Usuario_ID.Correo;
                    TemUsuario.Contraseña = Usuario_ID.Contraseña;
                    TemUsuario.Rol = Usuario_ID.Rol;
                    return View(TemUsuario);
                }
            }
            catch (Exception ex)
            {
                return View();
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Editar(Usuario TemUsuario) {
            try
            {
                using (Base_Datos_App1Entities DataBase = new Base_Datos_App1Entities())
                {
                    var EditUsuario = DataBase.Tabla_1.Find(TemUsuario.ID);
                    EditUsuario.Nombre = TemUsuario.Nombre;
                    EditUsuario.Documento = TemUsuario.Documento;
                    EditUsuario.Tipo_Documento = Request.Form["tipoDoc"];
                    EditUsuario.Correo = TemUsuario.Correo;
                    EditUsuario.Contraseña = TemUsuario.Contraseña;
                    EditUsuario.Rol = TemUsuario.Rol;

                    DataBase.Entry(EditUsuario).State = System.Data.Entity.EntityState.Modified;
                    DataBase.SaveChanges();
                    return Redirect("~/Admin/Lista_Usuarios");
                }
            }
            catch (Exception ex)
            {
                return Redirect("~/Admin/View_Editar");
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Eliminar(int ID)
        {
            try
            {
                using (Base_Datos_App1Entities DataBase = new Base_Datos_App1Entities())
                {
                    var DeletUsuario = DataBase.Tabla_1.Find(ID);
                    DataBase.Tabla_1.Remove(DeletUsuario);
                    DataBase.SaveChanges();
                }
                return Redirect("~/Admin/Lista_Usuarios");
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
