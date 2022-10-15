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
    //Llenar el combo de paises
    LlenarComboPais();
    LlenarGridCiudades();
});
function LlenarGridCiudades() {
    LlenarGridControlador("../Comunes/ControladorGrids.ashx", "TablaCiudades", null, "#tblCiudades");
}
function ProcesarComandos(Comando) {
    var Departamento = $("#cboDepartamento").val();
    var Codigo = $("#txtCodigo").val();
    if (Comando == "Insertar") { Codigo = 0;}
    var Nombre = $("#txtCiudad").val();
    var Activo = $("#chkActivo").prop('checked');

    var DatosCiudad = {
        Codigo: Codigo,
        Nombre: Nombre,
        Activo: Activo,
        CodigoDepartamento: Departamento,
        Comando: Comando
    }
    $.ajax({
        //Función Ajax
        type: "POST",
        url: "../Controladores/ControladorCiudad.ashx",
        contentType: "json",
        data: JSON.stringify(DatosCiudad),
        success: function (RptaCiudad) {
            if (Comando != "Consultar") {
                //Hay que procesar la respuesta para identificar si hay un error
                $("#dvMensaje").addClass("alert alert-success");
                $("#dvMensaje").html(RptaCiudad);
                LlenarGridCiudades();
            }
            else {
                let InfoCiudad = JSON.parse(RptaCiudad);
                $("#txtCodigo").val(InfoCiudad.Codigo);
                $("#txtCiudad").val(InfoCiudad.Nombre);
                $("#cboPais").val(InfoCiudad.CodigoPais);
                let Departamento = InfoCiudad.CodigoDepartamento;
                //Llenar el combo de departamento
                LlenarComboDepartamento(Departamento);
                $("#chkActivo").prop('checked', InfoCiudad.Activo);
            }
        },
        error: function (RespuestaError) {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html(RespuestaError);
        }
    });
}
function LlenarComboPais() {
    var promise = LlenarComboControlador("../Comunes/ControladorCombos.ashx", "PAIS", null, "#cboPais");

    if (promise) {
        promise.then(function (value) {
            //Se invoca el llenado del combo de producto
            LlenarComboDepartamento(0);
        });
    }
}
function LlenarComboDepartamento(CodDepto) {
    //Limpiar el combo de departamento
    $("#cboDepartamento").empty();
    let Pais = $("#cboPais").val();
    var lstParametros = [{ "Parametro": "@prPais", "Valor": Pais }];
    var promise = LlenarComboControlador("../Comunes/ControladorCombos.ashx", "DEPARTAMENTO", lstParametros, "#cboDepartamento");
    if (promise) {
        promise.then(function (value) {
            if (CodDepto > 0) {
                $("#cboDepartamento").val(CodDepto);
            }
        });
    }
}