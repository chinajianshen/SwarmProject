import Vue from "vue";
document.addEventListener('DOMContentLoaded', function (event) {
    let view = new Vue({
        el: document.getElementById('view'),
        mounted: function () { },
        data: {
            message: "One-way binding msg",
            twoWayBindingMessage: "Type here ..."
        }
    });
});
