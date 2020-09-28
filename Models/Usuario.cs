using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio1_Aplicación_base.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string Correo { get; set; }
        public string Tipo_Documento { get; set; }
        public string Documento { get; set; }
        public string Contraseña { get; set; }
    }
}