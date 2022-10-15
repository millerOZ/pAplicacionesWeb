using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Utiliza el namespace de cliente
using pAplicacionesWEB.Modelos;
//using Libcomunes.capadatos, para procesar comandos en la base de datos
using libComunes.CapaDatos;

namespace pAplicacionesWEB.Clases
{
    //Esta clase es la que va a gestionar los métodos del modelo cliente
    public class clsCliente
    {
        //Recibe la propiedad de cliente
        public Cliente _cliente { get; set; }
        public bool Consultar()
        {
            string SQL = "SELECT      Nombre, PrimerApellido, SegundoApellido, Direccion, Telefono, Email, " +
                                     "FechaNacimiento " +
                         "FROM        Cliente " +
                         "WHERE       Documento       = @prDocumento";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", _cliente.Documento);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    //Primero hay que poner a leer el reader
                    oConexion.Reader.Read();
                    //Hay información y se captura
                    _cliente.Nombre = oConexion.Reader.GetString(0);
                    _cliente.PrimerApellido = oConexion.Reader.GetString(1);
                    _cliente.SegundoApellido = oConexion.Reader.GetString(2);
                    _cliente.Direccion = oConexion.Reader.GetString(3);
                    _cliente.Telefono = oConexion.Reader.GetString(4);
                    _cliente.Email = oConexion.Reader.GetString(5);
                    _cliente.FechaNacimiento = oConexion.Reader.GetDateTime(6);
                    _cliente.Error = "";
                    return true;
                }
                else
                {
                    //No hay datos, se levanta un error
                    _cliente.Error = "No hay datos para el cliente con documento: " + _cliente.Documento;
                    return false;
                }
            }
            else
            {
                _cliente.Error = oConexion.Error;
                return false;
            }
        }
        public bool Eliminar()
        {
            //Método para grabar en la base de datos
            string SQL = "DELETE FROM        Cliente " +
                         "WHERE         Documento       = @prDocumento";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", _cliente.Documento);

            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                _cliente.Error = oConexion.Error;
                return false;
            }
        }
        public bool Actualizar()
        {
            //Método para grabar en la base de datos
            string SQL = "UPDATE        Cliente " +
                         "SET           Nombre          = @prNombre, " +
                                       "PrimerApellido  = @prPrimerApellido, " +
                                       "SegundoApellido = @prSegundoApellido, " +
                                       "Direccion       = @prDireccion, " +
                                       "Telefono        = @prTelefono, " +
                                       "Email           = @prEmail, " +
                                       "FechaNacimiento = @prFechaNacimiento " +
                         "WHERE         Documento       = @prDocumento";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", _cliente.Documento);
            oConexion.AgregarParametro("@prNombre", _cliente.Nombre);
            oConexion.AgregarParametro("@prPrimerApellido", _cliente.PrimerApellido);
            oConexion.AgregarParametro("@prSegundoApellido", _cliente.SegundoApellido);
            oConexion.AgregarParametro("@prDireccion", _cliente.Direccion);
            oConexion.AgregarParametro("@prTelefono", _cliente.Telefono);
            oConexion.AgregarParametro("@prEmail", _cliente.Email);
            oConexion.AgregarParametro("@prFechaNacimiento", _cliente.FechaNacimiento);

            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                _cliente.Error = oConexion.Error;
                return false;
            }
        }
        public bool Insertar()
        {
            //Método para grabar en la base de datos
            string SQL = "INSERT INTO Cliente (Documento, Nombre, PrimerApellido, SegundoApellido, Direccion, Telefono, Email, FechaNacimiento) " +
                         "VALUES (@prDocumento, @prNombre, @prPrimerApellido, @prSegundoApellido, @prDireccion, @prTelefono, @prEmail, @prFechaNacimiento)";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", _cliente.Documento);
            oConexion.AgregarParametro("@prNombre", _cliente.Nombre);
            oConexion.AgregarParametro("@prPrimerApellido", _cliente.PrimerApellido);
            oConexion.AgregarParametro("@prSegundoApellido", _cliente.SegundoApellido);
            oConexion.AgregarParametro("@prDireccion", _cliente.Direccion);
            oConexion.AgregarParametro("@prTelefono", _cliente.Telefono);
            oConexion.AgregarParametro("@prEmail", _cliente.Email);
            oConexion.AgregarParametro("@prFechaNacimiento", _cliente.FechaNacimiento);

            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                _cliente.Error = oConexion.Error;
                return false;
            }
        }
    }
}