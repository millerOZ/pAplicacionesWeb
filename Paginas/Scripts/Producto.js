//Código para garantizar que se ejecute el código cuando termine de cargar la página
$(document).ready(function () {
    //Defino la funcionalidad del botón "Actualizar"
    $("#btnActualizar").click(function () {
        ProcesarComandos("Actualizar");
    });
    //Defino la funcionalidad de la página
    //Funcionalidad del botón "Registrar"
    $("#btnInsertar").click(function () {
        ProcesarComandos("Insertar");
    });
    $("#btnEliminar").click(function () {
        ProcesarComandos("Eliminar");
    });
    $("#btnConsultar").click(function () {
        ProcesarComandos("Consultar");
    });
    //Llenar el combo de tipo producto
    LlenarComboTipoProducto();
});

function LlenarComboTipoProducto() {
    //Se invoca la función del javascript compartido "Combos.js" que está en la carpeta comunes
    //la cual invoca la ruta url con el comando para generar un json de respuesta con objetos de tipo 
    //Valor - Texto, que son llenados en un objeto tipo select
    LlenarComboControlador("../Comunes/ControladorCombos.ashx", "TipoProducto", null, "#cboTipoProducto");
    /*
     * var URL = "../Comunes/ControladorCombos.ashx";
    var Comando = "TipoProducto";
    var selectHTML = "#cboTipoProducto"
    LlenarComboControlador(URL, Comando, null, selectHTML);
    */
}

function LlenarTablaProductos() {
}

function ProcesarComandos(Comando) {
    var Codigo = $("#txtCodigoProducto").val();
    var Nombre = $("#txtProducto").val();
    var Descripcion = $("#txtDescripcion").val();
    var Cantidad = $("#txtInventario").val();
    var ValorUnitario = $("#txtValorUnitario").val();
    var TipoProducto = $("#cboTipoProducto").val();

    var DatosProducto = {
        Codigo: Codigo,
        Nombre: Nombre,
        Descripcion: Descripcion,
        Cantidad: Cantidad,
        ValorUnitario: ValorUnitario,
        TipoProducto: TipoProducto,
        Comando: Comando
    }
    $.ajax({
        //Función Ajax
        type: "POST",
        url: "../Controladores/ControladorProducto.ashx",
        contentType: "json",
        data: JSON.stringify(DatosProducto),
        success: function (RptaProductos) {
            if (Comando != "Consultar") {
                //Hay que procesar la respuesta para identificar si hay un error
                $("#dvMensaje").addClass("alert alert-success");
                $("#dvMensaje").html(RptaProductos);
                LlenarTablaProductos();
            }
            else {
                $("#txtCodigoProducto").val(RptaProductos.Codigo);
                $("#txtProducto").val(RptaProductos.Nombre);
                $("#txtDescripcion").val(RptaProductos.Descripcion);
                $("#txtInventario").val(RptaProductos.Cantidad);
                $("#txtValorUnitario").val(RptaProductos.ValorUnitario);
                $("#cboTipoProducto").val(RptaProductos.TipoProducto);
            }
        },
        error: function (RespuestaError) {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html(RespuestaError);
        }
    });
}