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
    /// Descripción breve de ControladorCiudad
    /// </summary>
    public class ControladorCiudad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string DatosCiudad;
                StreamReader reader = new StreamReader(context.Request.InputStream);
                DatosCiudad = reader.ReadToEnd();
                Ciudad ciudad = JsonConvert.DeserializeObject<Ciudad>(DatosCiudad);

                context.Response.Write(ProcesarComando(ciudad));
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }
        private string ProcesarComando(Ciudad ciudad)
        {
            clsCiudad oCiudad = new clsCiudad();
            oCiudad.ciudad = ciudad;
            switch (ciudad.Comando.ToUpper())
            {
                case "INSERTAR":
                    return oCiudad.Insertar();
                case "ACTUALIZAR":
                    return oCiudad.Actualizar();
                case "ELIMINAR":
                    return oCiudad.Eliminar();
                case "CONSULTAR":
                    if (oCiudad.Consultar())
                    {
                        return JsonConvert.SerializeObject(oCiudad.ciudad);
                    }
                    else
                    {
                        return oCiudad.ciudad.Error;
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