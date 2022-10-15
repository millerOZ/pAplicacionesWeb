using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pAplicacionesWEB.Modelos;
using libComunes.CapaDatos; //Hacer la conexión a la base de datos

namespace pAplicacionesWEB.Clases
{
    public class clsProducto
    {
        public Producto producto { get; set; }
        public string Insertar()
        {
            //Invocar el método insertar
            //Método para grabar en la base de datos
            string SQL = "Producto_Insertar"; //Nombre del procedimiento almacenado

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;//Para indicar que es un procedimiento almacenado
            oConexion.AgregarParametro("@prNombre", producto.Nombre);
            oConexion.AgregarParametro("@prDescripcion", producto.Descripcion);
            oConexion.AgregarParametro("@prCantidad", producto.Cantidad);
            oConexion.AgregarParametro("@prValorUnitario", producto.ValorUnitario);
            oConexion.AgregarParametro("@prTipoProducto", producto.TipoProducto);

            if (oConexion.EjecutarSentencia())
            {
                return "Se insertó el producto en la base de datos";
            }
            else
            {
                producto.Error = oConexion.Error;
                return producto.Error;
            }
        }
        public string Actualizar()
        {
            //Invocar el método insertar
            //Método para grabar en la base de datos
            string SQL = "Producto_Actualizar"; //Nombre del procedimiento almacenado

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;//Para indicar que es un procedimiento almacenado
            oConexion.AgregarParametro("@prCodigo", producto.Codigo);
            oConexion.AgregarParametro("@prNombre", producto.Nombre);
            oConexion.AgregarParametro("@prDescripcion", producto.Descripcion);
            oConexion.AgregarParametro("@prCantidad", producto.Cantidad);
            oConexion.AgregarParametro("@prValorUnitario", producto.ValorUnitario);
            oConexion.AgregarParametro("@prTipoProducto", producto.TipoProducto);

            if (oConexion.EjecutarSentencia())
            {
                return "Se actualizó el producto en la base de datos";
            }
            else
            {
                producto.Error = oConexion.Error;
                return producto.Error;
            }
        }
        public string Eliminar()
        {
            //Invocar el método insertar
            //Método para grabar en la base de datos
            string SQL = "Producto_Eliminar"; //Nombre del procedimiento almacenado

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;//Para indicar que es un procedimiento almacenado
            oConexion.AgregarParametro("@prCodigo", producto.Codigo);

            if (oConexion.EjecutarSentencia())
            {
                return "Se eliminó el producto en la base de datos";
            }
            else
            {
                producto.Error = oConexion.Error;
                return producto.Error;
            }
        }
        public bool Consultar()
        {
            string SQL = "Producto_Consultar";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;//Para indicar que es un procedimiento almacenado
            oConexion.AgregarParametro("@prCodigo", producto.Codigo);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    //Primero hay que poner a leer el reader
                    oConexion.Reader.Read();
                    //Hay información y se captura
                    producto.Nombre = oConexion.Reader.GetString(0);
                    producto.Descripcion = oConexion.Reader.GetString(1);
                    producto.Cantidad = oConexion.Reader.GetInt32(2);
                    producto.ValorUnitario = oConexion.Reader.GetInt32(3);
                    producto.TipoProducto = oConexion.Reader.GetInt32(4);
                    return true;
                }
                else
                {
                    //No hay datos, se levanta un error
                    producto.Error = "No hay datos para el producto con código: " + producto.Codigo;
                    return false;
                }
            }
            else
            {
                producto.Error = oConexion.Error;
                return false;
            }
        }
    }
}