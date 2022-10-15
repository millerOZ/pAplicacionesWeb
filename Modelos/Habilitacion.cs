using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pAplicacionesWEB.Modelos
{
    public class Habilitacion
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Asignatura { get; set; }
        public DateTime FechaHabilitacion { get; set; }
        public double NotaTeorica { get; set; }
        public double NotaPractica { get; set; }
        public double NotaCalculada { get; set; }
        public double NotaDefinitiva { get; set; }
        public string Error { get; set; }
      }
}