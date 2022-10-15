//Código para garantizar que se ejecute el código cuando termine de cargar la página
$(document).ready(function () {
    //Defino la funcionalidad de la página
    //Funcionalidad del botón "Registrar"
    $("#btnRegistrar").click(function () {
        var Documento = $("#txtDocumento").val();
        var Nombre = $("#txtNombre").val();
        var Asignatura = $("#cboAsignatura").val();
        var FechaHabilitacion = $("#txtFechaHabilitacion").val();
        var NotaTeorica = $("#txtNotaTeorica").val();
        var NotaPractica = $("#txtNotaPractica").val();

        var DatosHabilitacion = {
            Documento: Documento,
            Nombre: Nombre,
            Asignatura: Asignatura,
            FechaHabilitacion: FechaHabilitacion,
            NotaTeorica: NotaTeorica,
            NotaPractica: NotaPractica
        }
        $.ajax({
            //Función Ajax
            type: "POST",
            url: "../Controladores/ControladorHabilitacion.ashx",
            contentType: "json",
            data: JSON.stringify(DatosHabilitacion),
            success: function (RespuestaHabilitacion) {
                var Habilitacion = JSON.parse(RespuestaHabilitacion);
                if (Habilitacion.Error != "") {
                    ("#dvMensaje").addClass("alert alert-danger");
                    $("#dvMensaje").html(Habilitacion.Error);
                }
                else {
                    //Escribir la respuesta en los textbox de nota calculada y nota definitiva
                    $("#txtNotaCalculada").val(Habilitacion.NotaCalculada);
                    $("#txtNotaDefinitiva").val(Habilitacion.NotaDefinitiva);
                }
            },
            error: function (RespuestaError) {
                $("#dvMensaje").addClass("alert alert-danger");
                $("#dvMensaje").html(RespuestaError);
            }
        });
    });
});
