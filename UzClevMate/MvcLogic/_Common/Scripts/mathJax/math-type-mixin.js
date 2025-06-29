Vue.mixin({
    methods: {
        refreshMathJax() {
            if ('MathJax' in window) {
                setTimeout(() => this.$nextTick(function () {
                    MathJax.Hub.Queue(["Typeset", MathJax.Hub, document.body])
                }), 10);
            }
        },
    }
})