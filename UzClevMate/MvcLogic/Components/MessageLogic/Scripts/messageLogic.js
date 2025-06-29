$(function () {
    if (!window.common)
        window.common = {};

    window.common.messagingApi = new Vue({
        el: '#commonMessageToast',
        data() {
            return {
                header: null,
                text: null,
                cssClass: null
            };
        },

        methods: {

            showMessage(header, text) {
                this.cssClass = "success";
                this.showCommonMessage(header, text);
            },

            showWarning(header, text) {
                this.cssClass = "warning";
                this.showCommonMessage(header, text);
            },

            showError(header, text) {
                this.cssClass = "danger";
                this.showCommonMessage(header, text);
            },

            showCommonMessage(header, text) {
                this.text = null;
                this.header = null;
                const toastLiveExample = document.getElementById('liveToast')
                const toast = new bootstrap.Toast(toastLiveExample)
                toast.show()
                this.header = header;
                if (text) {
                    this.text = text;
                }
            }
        }
    });
});