using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pAplicacionesWEB.Modelos
{
    public class Cliente
    {
        //Es el modelo de ciente, con sólo propiedades, sin funcionalidad
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Error { get; set; }
        public string Comando { get; set; }
    }
}