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
    public class clsHabilitacion
    {
        public Habilitacion _habilitacion { get; set; }
        public bool Insertar()
        {
            //Manda a calcular las notas definitiva y calculada
            CalcularNotas();
            //Método para grabar en la base de datos
            string SQL = "INSERT INTO tblHabilitacion (Documento, Nombre, Asignatura, FechaHabilitacion, NotaTeorica, NotaPractica, NotaCalculada, NotaDefinitiva) " +
                         "VALUES (@prDocumento, @prNombre, @prAsignatura, @prFechaHabilitacion, @prNotaTeorica, @prNotaPractica, @prNotaCalculada, @prNotaDefinitiva)";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", _habilitacion.Documento);
            oConexion.AgregarParametro("@prNombre", _habilitacion.Nombre);
            oConexion.AgregarParametro("@prAsignatura", _habilitacion.Asignatura);
            oConexion.AgregarParametro("@prFechaHabilitacion", _habilitacion.FechaHabilitacion);
            oConexion.AgregarParametro("@prNotaTeorica", _habilitacion.NotaTeorica);
            oConexion.AgregarParametro("@prNotaPractica", _habilitacion.NotaPractica);
            oConexion.AgregarParametro("@prNotaCalculada", _habilitacion.NotaCalculada);
            oConexion.AgregarParametro("@prNotaDefinitiva", _habilitacion.NotaDefinitiva);

            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                _habilitacion.Error = oConexion.Error;
                return false;
            }
        }
        private void CalcularNotas()
        {
            double PorcentajeTeorica = 0.35;
            double PorcentajePractica = 0.65;

            _habilitacion.NotaCalculada = _habilitacion.NotaTeorica * PorcentajeTeorica +
                                          _habilitacion.NotaPractica * PorcentajePractica;

            if (_habilitacion.NotaCalculada <= 3)
            {
                _habilitacion.NotaDefinitiva = _habilitacion.NotaCalculada;
            }
            else
            {
                if (_habilitacion.NotaCalculada <= 4)
                {
                    _habilitacion.NotaDefinitiva = 3.0;
                }
                else { _habilitacion.NotaDefinitiva = 3.5; }
            }
        }
    }
}