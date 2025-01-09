let indice = 1;
let indice_slider_colecciona = 1;
let indice_slider_ebook = 1;
let indice_slider_default = 1;

muestraSlides(indice);
muestraSlideColeccion(indice_slider_colecciona);
muestraSlideEBook(indice_slider_ebook);


function avanzaSlide(n){
    muestraSlides( indice+=n );
}

function posicionSlide(n){
    muestraSlides(indice=n);
}

function avanzaSlideColeccion(n) {
    muestraSlideColeccion(indice_slider_colecciona += n);
}

function avanzaSlideEBook(n) {
    muestraSlideEBook(indice_slider_ebook += n);
}

function avanzaSlideDefault(n) {
    muestraSlideDefault(indice_slider_default += n);
}


var intervalSlider;
function iniciarIntervalSlider() {
     intervalSlider = setInterval(function tiempo() {
        muestraSlides(indice += 1)
    }, 10000);

}

//iniciarIntervalSlider();


setInterval(function tiempo() {
    muestraSlideColeccion(indice_slider_colecciona += 1)
}, 10000);

setInterval(function tiempo() {
    muestraSlideEBook(indice_slider_ebook += 1)
}, 10000);




function muestraSlides(n) {

    let i;
    let slides = document.getElementsByClassName('miSlider');
    let barras = document.getElementsByClassName('barra');

    if(n > slides.length){
        indice = 1;
    }
    if(n < 1){
        indice = slides.length;
    }
    for(i = 0; i < slides.length; i++){
        slides[i].style.display = 'none';
    }
    for(i = 0; i < barras.length; i++){
        barras[i].className = barras[i].className.replace(" limedica-active", "");
    }

    if (slides.length > 0) {
        slides[indice - 1].style.display = 'block';
        barras[indice - 1].className += ' limedica-active';
    }
}


function muestraSlideColeccion(n) {

    let i;
    let slidesOtros = document.getElementsByClassName('slider-coleccion');
    

    if (n > slidesOtros.length) {
        indice_slider_colecciona = 1;
    }
    if (n < 1) {
        indice_slider_colecciona = slidesOtros.length;
    }
    for (i = 0; i < slidesOtros.length; i++) {
        slidesOtros[i].style.display = 'none';
    }

    if (slidesOtros.length > 0) {
        slidesOtros[indice_slider_colecciona - 1].style.display = 'block';
    }

}


function muestraSlideEBook(n) {

    let i;
    let slidesEBook = document.getElementsByClassName('slider-ebook');


    if (n > slidesEBook.length) {
        indice_slider_ebook = 1;
    }
    if (n < 1) {
        indice_slider_ebook = slidesEBook.length;
    }
    for (i = 0; i < slidesEBook.length; i++) {
        slidesEBook[i].style.display = 'none';
    }

    if (slidesEBook.length > 0) {
        slidesEBook[indice_slider_ebook - 1].style.display = 'block';
    }


}


$(".miSlider").on("mouseenter", function () {
    clearInterval(intervalSlider);

});

$(".direcciones").on("mouseenter", function () {
    clearInterval(intervalSlider);

});
$(".miSlider").on("mouseleave", function () {
    iniciarIntervalSlider();

});

