using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Agregar en el using, system.io para procesar el flujo de información que llega del cliente
using System.IO;
//Agregar en el using, la carpeta de modelos
using pAplicacionesWEB.Modelos;
//Agregar using Newtonsoft.Json: Procesar la información
using Newtonsoft.Json;
using pAplicacionesWEB.Clases;

namespace pAplicacionesWEB.Controladores
{
    /// <summary>
    /// Summary description for ControladorProducto
    /// </summary>
    public class ControladorProducto : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string DatosProducto;
                StreamReader reader = new StreamReader(context.Request.InputStream);
                DatosProducto = reader.ReadToEnd();
                Producto producto = JsonConvert.DeserializeObject<Producto>(DatosProducto);

                context.Response.Write(ProcesarComando(producto));
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }
        private string ProcesarComando(Producto producto)
        {
            clsProducto oProducto = new clsProducto();
            oProducto.producto = producto;
            switch (producto.Comando.ToUpper())
            {
                case "INSERTAR":
                    return oProducto.Insertar();
                case "ACTUALIZAR":
                    return oProducto.Actualizar();
                case "ELIMINAR":
                    return oProducto.Eliminar();
                case "CONSULTAR":
                    if (oProducto.Consultar())
                    {
                        return JsonConvert.SerializeObject(oProducto.producto);
                    }
                    else
                    {
                        return oProducto.producto.Error;
                    }
                default:
                    return "No se ha definido el comando";
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}