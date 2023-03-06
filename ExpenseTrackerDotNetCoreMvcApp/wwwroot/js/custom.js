function toNumberStr(x) {
    return x.replace(/,(?=\d{3})/g, "");
}


function isNullOrEmpty(val) {
    //if (val == null || val == undefined || val == '')
    //    return true;
    //return false;

    return (val == null || val == undefined || val == '' || val == 0 || val < 0);
}

//function numbersOnly(event) {
//    var charCode = event.keyCode || event.which;
//    var charStr = String.fromCharCode(charCode);
//    if (!/^\d*\.?\d*$/.test(charStr)) {
//        event.preventDefault();
//    }
//}

(function ($) {
    $.fn.inputFilter = function (callback, errMsg) {
        return this.on("input keydown keyup mousedown mouseup select contextmenu drop focusout", function (e) {
            if (callback(this.value)) {
                // Accepted value
                if (["keydown", "mousedown", "focusout"].indexOf(e.type) >= 0) {
                    $(this).removeClass("input-error");
                    this.setCustomValidity("");
                }
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                // Rejected value - restore the previous one
                $(this).addClass("input-error");
                this.setCustomValidity(errMsg);
                this.reportValidity();
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                // Rejected value - nothing to restore
                this.value = "";
            }
        });
    };
}(jQuery));

function setDatePicker(id) {
    $(id).datepicker({
        format: 'yyyy-mm-dd',
        autoHide: true,
        endDate: "today"
    });

}
