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
    /// Summary description for ControladorCliente
    /// </summary>
    public class ControladorCliente : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string DatosCliente;
                StreamReader reader = new StreamReader(context.Request.InputStream);
                DatosCliente = reader.ReadToEnd();
                //La información que se pasa desde el cliente html, llega en el inputstream y se guarda como texto (string) en la variable datos cliente
                //Vamos a convertir el texto que está en la variable DatosCliente, a una clase: del modelo Cliente
                //Se hace con las clases de Newtonsoft, utilizar la clase jsonconvert
                Cliente _Cliente = JsonConvert.DeserializeObject<Cliente>(DatosCliente);

                switch(_Cliente.Comando.ToUpper())
                {
                    case "INSERTAR":
                        //Enviar a grabar a la base de datos
                        context.Response.Write(Insertar(_Cliente));
                        break;
                    case "ACTUALIZAR":
                        //Enviar a grabar a la base de datos
                        context.Response.Write(Actualizar(_Cliente));
                        break;
                    case "ELIMINAR":
                        //Enviar a grabar a la base de datos
                        context.Response.Write(Eliminar(_Cliente));
                        break;
                    case "CONSULTAR":
                        //Enviar a grabar a la base de datos
                        context.Response.Write(Consultar(_Cliente));
                        break;
                }
            }
            catch(Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }
        private string Consultar(Cliente _cliente)
        {
            //Invoca la clase clsCliente para que grabe la información en la base de datos
            clsCliente oCliente = new clsCliente();
            oCliente._cliente = _cliente;
            if (oCliente.Consultar())
            {
                //Se debe devolver el objeto cliente, en un formato de string (Texto), con estructura json
                //que el cliente sea capaz de leer
                return JsonConvert.SerializeObject(oCliente._cliente);
            }
            else
            {
                return oCliente._cliente.Error;
            }
        }
        private string Eliminar(Cliente _cliente)
        {
            //Invoca la clase clsCliente para que grabe la información en la base de datos
            clsCliente oCliente = new clsCliente();
            oCliente._cliente = _cliente;
            if (oCliente.Eliminar())
            {
                return "Se eliminó el cliente en la base de datos";
            }
            else
            {
                return oCliente._cliente.Error;
            }
        }
        private string Insertar(Cliente _cliente)
        {
            //Invoca la clase clsCliente para que grabe la información en la base de datos
            clsCliente oCliente = new clsCliente();
            oCliente._cliente = _cliente;
            if (oCliente.Insertar())
            {
                return "Se insertó el cliente en la base de datos";
            }
            else
            {
                return oCliente._cliente.Error;
            }
        }
        private string Actualizar(Cliente _cliente)
        {
            clsCliente oCliente = new clsCliente();
            oCliente._cliente = _cliente;
            if (oCliente.Actualizar())
            {
                return "Se actualizó el cliente en la base de datos";
            }
            else
            {
                return oCliente._cliente.Error;
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