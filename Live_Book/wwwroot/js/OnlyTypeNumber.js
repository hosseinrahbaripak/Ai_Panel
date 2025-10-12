function AllowOnlyNumbers(e) {
    e = (e) ? e : window.event; var key = null; var charsKeys = [97, 65, 99, 67, 118, 86, 115, 83, 112, 80]; var specialKeys = [8, 9, 27, 13, 35, 36, 37, 39, 46, 45]; key = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode; if (key && key < 48 || key > 57) {
        if ((e.ctrlKey && charsKeys.indexOf(key) != -1) || (navigator.userAgent.indexOf("Firefox") != -1 && ((e.ctrlKey && e.keyCode && e.keyCode > 0 && key >= 112 && key <= 123) || (e.keyCode && e.keyCode > 0 && key && key >= 112 && key <= 123)))) { return !0 }
        else if (specialKeys.indexOf(key) != -1) {
            if ((key == 39 || key == 45 || key == 46)) { return (navigator.userAgent.indexOf("Firefox") != -1 && e.keyCode != undefined && e.keyCode > 0) }
            else if (e.shiftKey && (key == 35 || key == 36 || key == 37)) { return !1 }
            else { return !0 }
        }
        else { return !1 }
    }
    else { return !0 }
}