Vue.mixin({
    methods: {

        generateShortGuid() {
          return 'xxxx-xxxx-xxxx-xxxx'.replace(/x/g, function() {
              return Math.floor(Math.random() * 16).toString(16);
          });
        },

        generateLongGuid() {
            const hexValues = '0123456789abcdef';
            let guid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx';

            guid = guid.replace(/[xy]/g, function (c) {
                const r = Math.random() * 16 | 0;
                const v = c == 'x' ? r : (r & 0x3 | 0x8);
                return hexValues[v];
            });

            return guid;
        },
        
        sleep(ms) {
            return new Promise(resolve => setTimeout(resolve, ms));
        },

        initToolTips() {

            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl)
            });

            $('[data-toggle="tooltip"]').tooltip({
                html: true
            });
        },

        uploadImages(event, container) {

            for (let file of event.target.files) {
                if (file.size > 1024 * 1024 * 4) {
                    window.common.messagingApi.showError("Превышен допустимый размер файла", "Имя файла: " + file.name);
                    container = [];
                    break;
                }
                container.push(file);
            }

            event.target.value = "";
        },

        deleteUploadedImage(index, container) {
            container.splice(index, 1);
        },

        isEmptyOrSpaces(str) {
            return str === null || str.match(/^ *$/) !== null;
        },

        getRightWordForm(count, for1, for234, forRest) {
            if (count > 10 && count <= 20) {
                return forRest;
            }

            var modulo = count % 10;

            if (modulo == 1) {
                return for1;
            }
            else if (modulo == 2 || modulo == 3 || modulo == 4) {
                return for234;
            }
            else {
                return forRest;
            }
        }
    }
})