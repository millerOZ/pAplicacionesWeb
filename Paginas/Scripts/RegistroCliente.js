//Código para garantizar que se ejecute el código cuando termine de cargar la página
$(document).ready(function () {
    //Defino la funcionalidad del botón "Actualizar"
    $("#btnActualizar").click(function () {
        ProcesarComandos("Actualizar");
    });
    //Defino la funcionalidad de la página
    //Funcionalidad del botón "Registrar"
    $("#btnRegistrar").click(function () {
        ProcesarComandos("Insertar");
    });
    $("#btnEliminar").click(function () {
        ProcesarComandos("Eliminar");
    });
    $("#btnConsultar").click(function () {
        ProcesarComandos("Consultar");
    });
});

function ProcesarComandos(Comando) {
    var Documento = $("#txtDocumento").val();
    var Nombre = $("#txtNombre").val();
    var PrimerApellido = $("#txtPrimerApellido").val();
    var SegundoApellido = $("#txtSegundoApellido").val();
    var FechaNacimiento;
    if ($("#txtFechaNacimiento").val() == "") {
        FechaNacimiento = "1900/01/01";
    }
    else {
        FechaNacimiento = $("#txtFechaNacimiento").val();
    }
    var Email = $("#txtEmail").val();
    var Direccion = $("#txtDireccion").val();
    var Telefono = $("#txtTelefono").val();

    var DatosCliente = {
        Documento: Documento,
        Nombre: Nombre,
        PrimerApellido: PrimerApellido,
        SegundoApellido: SegundoApellido,
        Email: Email,
        FechaNacimiento: FechaNacimiento,
        Telefono: Telefono,
        Direccion: Direccion,
        Comando: Comando
    }
    $.ajax({
        //Función Ajax
        type: "POST",
        url: "../Controladores/ControladorCliente.ashx",
        contentType: "json",
        data: JSON.stringify(DatosCliente),
        success: function (RespuestaCliente) {
            if (Comando == "Consultar") {
                var Cliente = JSON.parse(RespuestaCliente);
                if (Cliente.Error != "") {
                    $("#dvMensaje").addClass("alert alert-danger");
                    $("#dvMensaje").html(Cliente.Error);
                }
                else {
                    $("#txtNombre").val(Cliente.Nombre);
                    $("#txtPrimerApellido").val(Cliente.PrimerApellido);
                    $("#txtSegundoApellido").val(Cliente.SegundoApellido);
                    $("#txtEmail").val(Cliente.Email);
                    $("#txtDireccion").val(Cliente.Direccion);
                    $("#txtTelefono").val(Cliente.Telefono);
                    var Fecha = Cliente.FechaNacimiento;
                    $("#txtFechaNacimiento").val(Fecha.split('T')[0]);
                }
            }
            else {
                //Hay que procesar la respuesta para identificar si hay un error
                $("#dvMensaje").addClass("alert alert-success");
                $("#dvMensaje").html(RespuestaCliente);
            }
        },
        error: function (RespuestaError) {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html(RespuestaError);
        }
    });
}