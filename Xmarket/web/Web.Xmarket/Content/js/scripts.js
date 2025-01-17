
    window.addEventListener('DOMContentLoaded', event => {


    // Enable tooltips globally
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Enable popovers globally
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });

    // Activate Bootstrap scrollspy for the sticky nav component
    const navStick = document.body.querySelector('#navStick');
    if (navStick) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#navStick',
            offset: 82,
        });
    }

   

});


function buscarLibros(urlForm, platform) {

    var criterio = "";

    if (platform == "movil") {
        criterio = $("#search").val();
    }
    else {
        criterio = $("#consulta").val();
    }

    var urlCompleto = urlForm + "?consulta=" + criterio;

    window.location.href = urlCompleto;
}


var letSucripcion = $("#suscriptionCorreo");


function registrarSuscripcion(form) {
    let email = letSucripcion.val();

    if (email =="") {

        $("#divSucripcion").addClass("lm-all-input-error");
        letSucripcion.focus();
        return ;
    }

    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

    var valid = emailPattern.test(email);

    if (valid == false) {
        $("#divSucripcion").addClass("lm-all-input-error");
        letSucripcion.focus();
        return ;
    } 



}

function validarCampoSucripcion(sender) {

    if ($(sender).val() != "") {
        $("#divSucripcion").removeClass("lm-all-input-error");
    }

}



