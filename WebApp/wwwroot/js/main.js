

$(document).ready(function () {

    $('#kt_aside_menu').hover(function () {
        $('body').addClass('kt-aside--minimize-hover')
        $('body').removeClass('kt-aside--minimize')
    }, function () {
        $('body').addClass('kt-aside--minimize')

        $('body').removeClass('kt-aside--minimize-hover')

    })

});