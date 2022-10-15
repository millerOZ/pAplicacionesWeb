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
    /// Descripción breve de ControladorHabilitacion
    /// </summary>
    public class ControladorHabilitacion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string DatosHabilitacion;
                StreamReader reader = new StreamReader(context.Request.InputStream);
                DatosHabilitacion = reader.ReadToEnd();
                //La información que se pasa desde el cliente html, llega en el inputstream y se guarda como texto (string) en la variable datos cliente
                //Vamos a convertir el texto que está en la variable DatosCliente, a una clase: del modelo Cliente
                //Se hace con las clases de Newtonsoft, utilizar la clase jsonconvert
                Habilitacion _Habilitacion = JsonConvert.DeserializeObject<Habilitacion>(DatosHabilitacion);

                //Enviar a grabar a la base de datos
                context.Response.Write(Insertar(_Habilitacion));

            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }
        private string Insertar(Habilitacion _habilitacion)
        {
            //Invoca la clase clsCliente para que grabe la información en la base de datos
            clsHabilitacion oHabilitacion = new clsHabilitacion();
            oHabilitacion._habilitacion = _habilitacion;
            if (oHabilitacion.Insertar())
            {
                //Retornamos el objeto habilitación, en formato de string con jsonconvert
                return JsonConvert.SerializeObject(oHabilitacion._habilitacion);
            }
            else
            {
                return JsonConvert.SerializeObject(oHabilitacion._habilitacion);
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