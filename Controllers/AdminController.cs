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
            using (Laboratorio1_AP_DataBaseEntities DataBase = new Laboratorio1_AP_DataBaseEntities()){ //leer base de datos
                foreach( var Elemento in DataBase.tablaUsuariosdB)
                {
                    Tem_List.List_Usuarios.Add(new Usuario {
                    ID=Elemento.ID,
                    Contraseña= Elemento.password,
                    Correo = Elemento.correo,
                    Documento=Elemento.numeroDocumento,
                    Tipo_Documento = Elemento.tipoDocumento,
                    Nombre = Elemento.nombre,
                    Rol = Elemento.rol
                    });
                }

            }
            return View(Tem_List);
        }

        public ActionResult View_Editar(int ID) {
            try
            {
                var TemUsuario = new Usuario();
                using (Laboratorio1_AP_DataBaseEntities DataBase = new Laboratorio1_AP_DataBaseEntities())
                {
                    var Usuario_ID = DataBase.tablaUsuariosdB.Find(ID);
                    TemUsuario.ID = ID;
                    TemUsuario.Nombre = Usuario_ID.nombre;
                    TemUsuario.Documento = Usuario_ID.numeroDocumento;
                    TemUsuario.Tipo_Documento = Usuario_ID.tipoDocumento;
                    TemUsuario.Correo = Usuario_ID.correo;
                    TemUsuario.Contraseña = Usuario_ID.password;
                    TemUsuario.Rol = Usuario_ID.rol;
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
                using (Laboratorio1_AP_DataBaseEntities DataBase = new Laboratorio1_AP_DataBaseEntities())
                {
                    var EditUsuario = DataBase.tablaUsuariosdB.Find(TemUsuario.ID);
                    EditUsuario.nombre = TemUsuario.Nombre;
                    EditUsuario.numeroDocumento = TemUsuario.Documento;
                    EditUsuario.tipoDocumento = Request.Form["tipoDoc"];
                    EditUsuario.correo = TemUsuario.Correo;
                    EditUsuario.password = TemUsuario.Contraseña;
                    EditUsuario.rol = TemUsuario.Rol;

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
                using (Laboratorio1_AP_DataBaseEntities DataBase = new Laboratorio1_AP_DataBaseEntities())
                {
                    var DeletUsuario = DataBase.tablaUsuariosdB.Find(ID);
                    DataBase.tablaUsuariosdB.Remove(DeletUsuario);
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
