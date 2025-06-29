$(document).ready(function () {

    //тултипы
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });

    $('[data-toggle="tooltip"]').tooltip({
        html: true
    });

    //скролл через якорь
    $('.scroll-link').on('click', function (e) {
        e.preventDefault();
        var href = $(this).attr('href');

        if ($(href).length) {
            $('html, body').animate({ scrollTop: $(href).offset().top }, 500);
        }
    });
});


