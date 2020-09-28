using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio1_Aplicación_base.Models;

namespace Laboratorio1_Aplicación_base.Controllers
{
    public class TimeController : Controller
    {
        // GET: Time
        public string getTime(int id)
        {
            try
            {
                var autorizedID = new List<int> { 1, 2, 3 };
                if (!autorizedID.Exists(X => X == id)) throw new Exception("ID no autorizado");
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }catch(Exception ex)
            {
                return "ERROR" + ex.Message;
            }
        }
        [HttpPost]
        public void setTime(int id, string date)
        {
            try
            {
                string Hora_Result = date;

                using (Laboratorio1_AP_DataBaseEntities DataBase = new Laboratorio1_AP_DataBaseEntities()) {
                    var Hora = new tablaTiempodB();
                    Hora.hora = TimeSpan.Parse(Hora_Result);
                    DataBase.tablaTiempodB.Add(Hora);
                    DataBase.SaveChanges();         
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ActionResult View_Hora() {
            var Tem_List = new Lista_Horas();
            Tem_List.List_Horas = new List<Date_ID>();
            using (Laboratorio1_AP_DataBaseEntities DataBase = new Laboratorio1_AP_DataBaseEntities()) {
                foreach (var Elementos in DataBase.tablaTiempodB) {
                    Tem_List.List_Horas.Add(new Date_ID
                    {
                        ID = Elementos.ID,
                        Hora = Elementos.hora
                    });
                }
            }

                return View(Tem_List);
        }
    }
}