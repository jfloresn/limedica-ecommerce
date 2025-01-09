
var urlProvincia;
var urlDistrito;

$(document).ready(function () {

     urlProvincia = $("#hidenUrlProvincia").data("url");
    urlDistrito = $("#hidenUrlDistrito").data("url");

    $("#ddlDepartamento").on("change", function () {
        listarProvincias($("#ddlDepartamento option:selected").val());
    });

    $("#ddlProvincia").on("change", function () {
        listarDistritos($("#ddlProvincia option:selected").val(), $("#ddlDepartamento option:selected").val());
    });

});


function listarProvincias(departamento) {

    var formProvincia = new FormData();
    formProvincia.append("nombreDepartamento", departamento);

 

    try {

        $.ajax({
            url: urlProvincia,
            data: formProvincia,
            type: 'POST',
            contentType: false,
            processData: false,
            cache: false,
            beforeSend: function () {

                $('#ddlProvincia').attr('disabled', 'disabled');
            },
            error: function (jqXHR, textStatus, errorThrown) {


                $('#ddlProvincia').removeAttr('disabled');

            },
            success: function (result) {


                $('#ddlProvincia').removeAttr('disabled');
                let hidenProvinciaValue = $("#hidenProvincia").val();
                let hidenDepartamentoValue = $("#ddlDepartamento").val();

                if (result != null) {

                    if (result.OperacionType.codigo_operacion == "0") {

                        $("#ddlProvincia").html("");
                        $("#ddlProvincia").append(new Option("Seleccionar Provincia", ""));

                        for (i = 0; i < result.OperacionType.Objeto1.length; i++) {

                            $("#ddlProvincia").append(new Option(result.OperacionType.Objeto1[i].nombreProvincia, result.OperacionType.Objeto1[i].nombreProvincia));

                        }
                    }

                    $("#ddlProvincia option[value='" + hidenProvinciaValue + "']").attr("selected", "selected");


                    if (hidenProvinciaValue != '') {
                        listarDistritos(hidenProvinciaValue, hidenDepartamentoValue);
                    }

                }

            },
            complete: function (data) {

            }
        });

    }
    catch (error) {

        alert(error);

    }

}

function listarDistritos(provincia, departamento) {

    var formProvincia = new FormData();
    formProvincia.append("nombreDepartamento", departamento);
    formProvincia.append("nombreProvincia", provincia);


    try {

        $.ajax({
            url: urlDistrito,
            data: formProvincia,
            type: 'POST',
            contentType: false,
            processData: false,
            cache: false,
            beforeSend: function () {

                $('#ddlDistrito').attr('disabled', 'disabled');
            },
            error: function (jqXHR, textStatus, errorThrown) {


                $('#ddlDistrito').removeAttr('disabled');




            },
            success: function (result) {

                let hidenDistritoValue = $("#hidenDistrito").val();

                $('#ddlDistrito').removeAttr('disabled');

                if (result != null) {

                    if (result.OperacionType.codigo_operacion == "0") {

                        $("#ddlDistrito").html("");
                        $("#ddlDistrito").append(new Option("Seleccionar Distrito", ""));

                        for (i = 0; i < result.OperacionType.Objeto1.length; i++) {

                            $("#ddlDistrito").append(new Option(result.OperacionType.Objeto1[i].nombreDistrito, result.OperacionType.Objeto1[i].codigoDistrito));

                        }
                    }

                    $("#ddlDistrito option[value='" + hidenDistritoValue + "']").attr("selected", "selected");
                }

            },
            complete: function (data) {

            }
        });

    }
    catch (error) {

        alert(error);

    }

}


function submitRegistrarDireccion(oFormElement) {

    var frmDataClienteDireccion = $("#frmRegistrarDireccion").serialize() ;
    var urlClienteDireccion = $("#hidenUrlRegistrarDireccion").data("url");

 
    $.ajax(
        {
            url: urlClienteDireccion,
            data: frmDataClienteDireccion,
            type: "POST",
            //contentType: false,
            processData: false,
            cache: false,
            beforeSend: function () {

                $("#btnRegistrarDireccion").prop("disabled",true);
            },
            success: function (data) {
                $("#btnRegistrarDireccion").prop("disabled", false);

                if (data.OperacionType.estado_operacion == "0") {
                    swal({ title: "Correcto", text: "Se registró correctamente", type: "success", confirmButtonText: "Aceptar" });
                    $("#modalNuevaDireccion").modal("hide");

                    direccionRegistrado(data.OperacionType.Objeto1);

                }
                else {
                    $(".validate-imput").html("");

                    for (let i = 0; i < data.errorForms.length; i++) {
                        $("#span_" + data.errorForms[i].propiedad).html(data.errorForms[i].mensaje);
                    }
                }

            },
            error: function (request, status, error) {

                swal({ title: "Error!", text: "Ocurrió un error al registrar", type: "error", confirmButtonText: "Aceptar" });
            }
        });

}


function registrarDireccion() {

    $("#frmRegistrarDireccion").submit();
}