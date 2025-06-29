$(function () {
    new Vue({
        el: '#admin-logs',

        data() {
            return {
                isLoading: false,
                message: null,
                logs: [],
                details: [],
                search: {
                    onlyErrors: false,
                    dateStart: $('#admin-logs').data('date-start'),
                    dateStartHour: 0,
                    dateEnd: null,
                    dateEndHour: 24,
                    userId: $('#admin-logs').data('user-id'),
                    methodName: 0
                }
            };
        },

        methods: {

            async getlogs() {
                this.message = null;

                if (!this.search.dateStart) {
                    this.message = "Введите дату поиска";
                    return;
                }

                this.isLoading = true;
                this.logs = [];

                let data = await $.ajax({
                    url: "/Log/Get",
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    data: JSON.stringify({
                        userId: this.search.userId,
                        dateStartStr: this.search.dateStart,
                        dateStartHour: this.search.dateStartHour,
                        dateEndStr: this.search.dateEnd,
                        dateEndHour: this.search.dateEndHour,
                        onlyErrors: this.search.onlyErrors,
                        methodName: this.search.methodName
                    })
                });

                if (!data.logs || !data.logs.length) {
                    this.message = "Ничего не найдено";
                }

                this.logs = data.logs;
                this.isLoading = false;
            },

            showLogDetails(index) {
                this.details = JSON.parse(this.logs[index].Log);
                $('#log-detail').modal('show');
            }
        }
    });
});