using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAPI.Models
{
    public class Empleados
    {
        public int ID_EMPLEADO { get; set; }
        public int FK_ID_AREA { get; set; }
        public string NOMBRE_EMPLEADO { get; set; }
        public string APELLIDOS_EMPLEADO { get; set; }
        public string CORREO_EMPLEADO { get; set; }
        public string DIRECCION_EMPLEADO { get; set; }
        public string TELEFONO_EMPLEADO { get; set; }

    }
}