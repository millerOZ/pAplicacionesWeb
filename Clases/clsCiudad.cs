using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pAplicacionesWEB.Modelos;
using libComunes.CapaDatos;

namespace pAplicacionesWEB.Clases
{
    public class clsCiudad
    {
        public Ciudad ciudad { get; set; }
        public string Insertar()
        {
            //Invocar el método insertar
            //Método para grabar en la base de datos
            string SQL = "Ciudad_Insertar"; //Nombre del procedimiento almacenado

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;//Para indicar que es un procedimiento almacenado
            oConexion.AgregarParametro("@prNombre", ciudad.Nombre);
            oConexion.AgregarParametro("@prActivo", ciudad.Activo);
            oConexion.AgregarParametro("@prDepartamento", ciudad.CodigoDepartamento);

            if (oConexion.EjecutarSentencia())
            {
                return "Se insertó la ciudad en la base de datos";
            }
            else
            {
                ciudad.Error = oConexion.Error;
                return ciudad.Error;
            }
        }
        public string Actualizar()
        {
            //Invocar el método insertar
            //Método para grabar en la base de datos
            string SQL = "Ciudad_Actualizar"; //Nombre del procedimiento almacenado

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;//Para indicar que es un procedimiento almacenado
            oConexion.AgregarParametro("@prCodigo", ciudad.Codigo);
            oConexion.AgregarParametro("@prNombre", ciudad.Nombre);
            oConexion.AgregarParametro("@prActivo", ciudad.Activo);
            oConexion.AgregarParametro("@prDepartamento", ciudad.CodigoDepartamento);

            if (oConexion.EjecutarSentencia())
            {
                return "Se actualizó la ciudad en la base de datos";
            }
            else
            {
                ciudad.Error = oConexion.Error;
                return ciudad.Error;
            }
        }
        public string Eliminar()
        {
            //Invocar el método insertar
            //Método para grabar en la base de datos
            string SQL = "Ciudad_Eliminar"; //Nombre del procedimiento almacenado

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;//Para indicar que es un procedimiento almacenado
            oConexion.AgregarParametro("@prCodigo", ciudad.Codigo);

            if (oConexion.EjecutarSentencia())
            {
                return "Se eliminó la ciudad en la base de datos";
            }
            else
            {
                ciudad.Error = oConexion.Error;
                return ciudad.Error;
            }
        }
        public bool Consultar()
        {
            string SQL = "Ciudad_Consultar";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;//Para indicar que es un procedimiento almacenado
            oConexion.AgregarParametro("@prCodigo", ciudad.Codigo);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    //Primero hay que poner a leer el reader
                    oConexion.Reader.Read();
                    //Hay información y se captura
                    ciudad.CodigoPais = oConexion.Reader.GetInt32(0);
                    ciudad.CodigoDepartamento = oConexion.Reader.GetInt32(2);
                    ciudad.Codigo = oConexion.Reader.GetInt32(3);
                    ciudad.Nombre = oConexion.Reader.GetString(4);
                    ciudad.Activo = oConexion.Reader.GetBoolean(5);
                    return true;
                }
                else
                {
                    //No hay datos, se levanta un error
                    ciudad.Error = "No hay datos para la ciudad con código: " + ciudad.Codigo;
                    return false;
                }
            }
            else
            {
                ciudad.Error = oConexion.Error;
                return false;
            }
        }
    }
}