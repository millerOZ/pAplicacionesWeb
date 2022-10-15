var oTabla = $("#tblProductos").DataTable();
//Código para garantizar que se ejecute el código cuando termine de cargar la página
$(document).ready(function () {

    $('#tblProductos tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            oTabla.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
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
    LlenarComboTipoProducto();
    LlenarGridProductos();
    $('#tblProductos tbody').on('click', 'tr', function () {
        EditarFila(oTabla.row(this).data());
    });
});
function EditarFila(DatosFila) {
    //Selecciona los datos de la fila seleccionada
    $("#cboTipoProducto").val(DatosFila[0]);
    
    $("#txtCodigoProducto").val(DatosFila[2]);
    $("#txtProducto").val(DatosFila[3]);
    $("#txtValorUnitario").val(DatosFila[6]);
    $("#txtInventario").val(DatosFila[5]);
    $("#txtDescripcion").val(DatosFila[4]);
}
function ProcesarComandos(Comando) {
    var Codigo = $("#txtCodigoProducto").val();
    var Nombre = $("#txtProducto").val();
    var ValorUnitario = $("#txtValorUnitario").val();
    var Cantidad = $("#txtInventario").val();
    var Descripcion = $("#txtDescripcion").val();
    var TipoProducto = $("#cboTipoProducto").val();

    var DatosProducto = {
        Codigo: Codigo,
        Nombre: Nombre,
        ValorUnitario: ValorUnitario,
        Cantidad: Cantidad,
        Descripcion: Descripcion,
        CodigoTipoProducto: TipoProducto,
        Comando: Comando
    }
    $.ajax({
        //Función Ajax
        type: "POST",
        url: "../Controladores/ProductoControlador.ashx",
        contentType: "json",
        data: JSON.stringify(DatosProducto),
        success: function (RespuestaProducto) {
            //Hay que procesar la respuesta para identificar si hay un error
            $("#dvMensaje").addClass("alert alert-success");
            $("#dvMensaje").html(RespuestaProducto);
            LlenarGridProductos();
        },
        error: function (RespuestaError) {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html(RespuestaError);
        }
    });
}

function LlenarComboTipoProducto() {
    LlenarComboControlador("../Comunes/ControladorCombos.ashx", "TIPOPRODUCTO", null, "#cboTipoProducto");
}

function LlenarGridProductos() {
    LlenarGridControlador("../Comunes/ControladorGrids.ashx", "Producto", null, "#tblProductos");
}