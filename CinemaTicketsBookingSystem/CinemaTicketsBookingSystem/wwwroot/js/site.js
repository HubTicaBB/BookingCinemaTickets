// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(window).on('resize', mobileSize);
$(window).on('load', mobileSize);

function mobileSize() {
    if ($(window).width() < 767) {
        $('.col-resp').removeClass('col-8');
        $('.col-resp').removeClass('col-lg-8');
        $('.input-resp').removeClass('col-2');
        $('.input-resp').addClass('col-6');
        $('.h-resp').hide();
    }
    else {
        $('.col-resp').addClass('col-8');
        $('.col-resp').addClass('col-lg-8');
        $('.input-resp').addClass('col-2');
        $('.input-resp').removeClass('col-6');
        $('.h-resp').show();
    }
}
