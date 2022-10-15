using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pAplicacionesWEB.Modelos
{
    public class Ciudad
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public int CodigoDepartamento { get; set; }
        public int CodigoPais { get; set; }
        public string Comando { get; set; }
        public string Error { get; set; }
    }
}