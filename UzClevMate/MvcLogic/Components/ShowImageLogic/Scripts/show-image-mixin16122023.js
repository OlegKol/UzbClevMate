Vue.mixin({
    methods: {
        refreshImageDom() {
            setTimeout(() =>  $('a.img-popup, a.img-popup-plain').click(function (event) {
                event.preventDefault();
                let addressValue = $(this).attr("href");
                $('#commmonImagePopupElement').attr("src", addressValue);
                $('#commonImagePopup').modal('show');
            }), 50);
        },
    }
})