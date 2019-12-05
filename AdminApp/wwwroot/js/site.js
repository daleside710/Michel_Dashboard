// Write your Javascript code.
$(document).ready(function () {
    //Ladda.bind('.btn', { timeout: 1000 });
});
function ShowMessage(msg) {
    toastr.options = {
        "debug": false,
        "positionClass": "toast-top-center",
        "onclick": null,
        "fadeIn": 300,
        "fadeOut": 1000,
        "timeOut": 5000,
        "extendedTimeOut": 1000
    }
    toastr.success(msg);
}

function ShowMessageError(msg) {
    toastr.options = {
        "debug": false,
        "positionClass": "toast-top-center",
        "onclick": null,
        "fadeIn": 300,
        "fadeOut": 1000,
        "timeOut": 5000,
        "extendedTimeOut": 1000
    }
    toastr.error(msg);
}
