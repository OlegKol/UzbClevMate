var taskComplaintMixin = {
    data() {
        return {
            taskComplaint: {
                text: null,
                isLoading: false,
                hasSendingResult: false,
                taskId: null,
                firstParam: null,
                secondParam: null
            }
        };
    },

    methods: {

        showComplaintModal(taskId, isTempalte, firstParam, secondParam) {
            this.taskComplaint.hasSendingResult = false;
            this.taskComplaint.firstParam = null;
            this.taskComplaint.secondParam = null;
            this.taskComplaint.text = null;
            this.taskComplaint.taskId = taskId;
            if(isTempalte){
                this.taskComplaint.firstParam = firstParam;
                this.taskComplaint.secondParam = secondParam;
            }
            $("#task-error").modal('show');
        },

        showComplaintModalForTelegramUser(telegramUserId, taskId, isTempalte, firstParam, secondParam) {
            this.taskComplaint.hasSendingResult = false;
            this.taskComplaint.firstParam = null;
            this.taskComplaint.secondParam = null;
            this.taskComplaint.text = null;
            this.taskComplaint.taskId = taskId;
            if(isTempalte){
                this.taskComplaint.firstParam = firstParam;
                this.taskComplaint.secondParam = secondParam;
            }
            $("#task-error").modal('show');
        },

        async sendComplaint() {
            this.taskComplaint.isLoading = true;

            await $.ajax({
                url: "/TaskComplaint/SendTaskComplaint",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                data: JSON.stringify({
                    taskId: this.taskComplaint.taskId,
                    text: this.taskComplaint.text,
                    firstParam: this.taskComplaint.firstParam,
                    secondParam: this.taskComplaint.secondParam
                })
            });

            this.taskComplaint.hasSendingResult = true;
            this.taskComplaint.isLoading = false;
        }
    }
}