using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pAplicacionesWEB.Modelos
{
    public class Producto
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int ValorUnitario { get; set; }
        public int TipoProducto { get; set; }
        public string Comando { get; set; }
        public string Error { get; set; }
    }
}